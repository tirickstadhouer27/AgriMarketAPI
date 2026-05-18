using Microsoft.AspNetCore.Mvc;
using AgriMarketAPI.Models;
using AgriMarketAPI.Models.Enums;
using AgriMarketAPI.Repositories;
using AgriMarketAPI.Exceptions;
using System;

namespace AgriMarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController : ControllerBase
    {
        // Instantiating our repository layer (Manual dependency injection for now)
        private static readonly ProduceRepository _repository = new ProduceRepository();

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("available")]
        public IActionResult GetAvailable() => Ok(_repository.GetAvailable());

        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(Category category) => Ok(_repository.GetByCategory(category));

        // TC-06 & TC-07: Using your custom exception with a try-catch block
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var listing = _repository.GetById(id);
                return Ok(listing);
            }
            catch (ListingNotFoundException ex)
            {
                // Returns clean 404 block with custom error message
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProduceListing newListing)
        {
            // Input Validation Guards
            if (string.IsNullOrWhiteSpace(newListing.ProductName) || newListing.ProductName.Length < 3)
            {
                return BadRequest(new { message = "Product name must be at least 3 characters long." });
            }
            if (newListing.PricePerKg <= 0)
            {
                return BadRequest(new { message = "Price per kilogram must be greater than zero." });
            }

            try
            {
                _repository.Add(newListing);
                return CreatedAtAction(nameof(GetById), new { id = newListing.ListingId }, newListing);
            }
            catch (FormatException ex)
            {
                return BadRequest(new { message = "Invalid data format provided.", details = ex.Message });
            }
        }
    }
}