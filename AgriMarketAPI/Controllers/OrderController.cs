using Microsoft.AspNetCore.Mvc;
using AgriMarketAPI.Models;
using AgriMarketAPI.Models.Enums;
using AgriMarketAPI.Repositories;
using AgriMarketAPI.Notifications;
using AgriMarketAPI.Interfaces;
using System;

namespace AgriMarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly OrderRepository _orderRepository = new OrderRepository();
        private static readonly ProduceRepository _produceRepository = new ProduceRepository();
        
        // Polymorphic notification engines adhering to INotifiable contract
        private readonly INotifiable _emailService = new EmailNotifier();
        private readonly INotifiable _smsService = new SmsNotifier();

        // TC-12 & TC-13: Place a new order
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order newOrder)
        {
            try
            {
                // Fetch the listing item being purchased from the repository layer
                var listing = _produceRepository.GetById(newOrder.ProduceListingId);

                // Guard Clause: Explicit null safety verification to prevent reference errors
                if (listing == null)
                {
                    return NotFound(new { message = $"Order failed. Produce listing with ID {newOrder.ProduceListingId} does not exist." });
                }

                // TC-13: Validation Guard - Reject if ordering more than available stock bounds
                if (newOrder.QuantityOrderedKg > listing.QuantityKg)
                {
                    return UnprocessableEntity(new { message = $"Order failed. Requested amount ({newOrder.QuantityOrderedKg}kg) exceeds available stock ({listing.QuantityKg}kg)." });
                }

                // Complete automatic business parameter assignments
                newOrder.TotalPrice = listing.PricePerKg * newOrder.QuantityOrderedKg;
                newOrder.OrderDate = DateTime.Now;
                newOrder.Status = OrderStatus.Pending;

                _orderRepository.Add(newOrder);

                // Run alert channels polymorphically
                _emailService.SendNotification("buyer@agrimarket.co.za", $"Your order #{newOrder.OrderId} is processing. Total: R{newOrder.TotalPrice}");

                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) 
            {
                return NotFound(new { message = $"Order #{id} not found." });
            }
            return Ok(order);
        }

        // TC-14: PATCH /api/orders/{id}/confirm
        [HttpPatch("{id}/confirm")]
        public IActionResult ConfirmOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) 
            {
                return NotFound(new { message = "Order not found." });
            }

            // Ensure the state transition workflow is strictly respected
            if (order.Status != OrderStatus.Pending)
            {
                return BadRequest(new { message = "Only Pending orders can be confirmed." });
            }

            order.Status = OrderStatus.Confirmed;
            _smsService.SendNotification("farmer@agrimarket.co.za", $"Order #{order.OrderId} confirmed. Prepare items for collection.");

            return Ok(new { message = "Order confirmed successfully.", status = order.Status.ToString() });
        }

        // TC-15: PATCH /api/orders/{id}/collect
        [HttpPatch("{id}/collect")]
        public IActionResult CollectOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) 
            {
                return NotFound(new { message = "Order not found." });
            }

            // Ensure collection can only take place once item is confirmed by farm operators
            if (order.Status != OrderStatus.Confirmed)
            {
                return BadRequest(new { message = "Only Confirmed orders can be marked as Collected." });
            }

            try
            {
                var listing = _produceRepository.GetById(order.ProduceListingId);
                
                // Guard Clause: Verify linked listing source hasn't been scrubbed before reducing stock
                if (listing == null)
                {
                    return NotFound(new { message = $"Collection failed. The linked produce listing with ID {order.ProduceListingId} no longer exists." });
                }
                
                // Deduct inventory quantities from the matching record inside memory state
                listing.QuantityKg -= order.QuantityOrderedKg;
                
                // If inventory hits zero, flip listing availability off automatically to avoid ghost orders
                if (listing.QuantityKg <= 0)
                {
                    listing.QuantityKg = 0;
                    listing.IsAvailable = false;
                }

                order.Status = OrderStatus.Collected;
                _emailService.SendNotification("buyer@agrimarket.co.za", $"Order #{order.OrderId} collected. Thank you for buying local!");

                return Ok(new { message = "Order picked up. Inventory stock updated successfully.", updatedStock = listing.QuantityKg });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Inventory link update failure.", details = ex.Message });
            }
        }
    }
}