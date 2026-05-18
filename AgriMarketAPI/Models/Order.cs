using System;
using AgriMarketAPI.Models.Enums;

namespace AgriMarketAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProduceListingId { get; set; }
        public int BuyerId { get; set; }
        public double QuantityOrderedKg { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        
        // Tracking the state workflow using our Enums
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Order() { }

        public Order(int orderId, int listingId, int buyerId, double quantity, double totalPrice)
        {
            OrderId = orderId;
            ProduceListingId = listingId;
            BuyerId = buyerId;
            QuantityOrderedKg = quantity;
            TotalPrice = totalPrice;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }
    }
}