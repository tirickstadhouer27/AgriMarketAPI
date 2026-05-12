using Microsoft.AspNetCore.Mvc;
using AgriMarketAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgriMarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController : ControllerBase
    {
        // Static In-Memory List (Ch. 6: Collections)
        // This persists as long as the application is running
        private static List<ProduceListing> _listings = new List<ProduceListing>
        {
            new ProduceListing(1, "Potatoes", "Vegetables", 15.50, 100, true),
            new ProduceListing(2, "Tomatoes", "Vegetables", 20.00, 50, false),
            new ProduceListing(3, "Apples", "Fruit", 25.00, 200, true)
        };

        // TC-01: GET all listings
        [HttpGet]
        public ActionResult<IEnumerable<ProduceListing>> GetAll()
        {
            return Ok(_listings);
        }

        // TC-03: POST a new listing
        [HttpPost]
        public ActionResult Create([FromBody] ProduceListing newListing)
        {
            _listings.Add(newListing);
            // Returns 201 Created status code
            return CreatedAtAction(nameof(GetAll), new { id = newListing.ListingId }, newListing);
        }

        // TC-04: GET only available items (Ch. 5: Selection/Filtering)
        [HttpGet("available")]
        public ActionResult<IEnumerable<ProduceListing>> GetAvailable()
        {
            var availableItems = _listings.Where(l => l.IsAvailable).ToList();
            return Ok(availableItems);
        }

        // TC-05: GET by Category (Ch. 5: Selection/Switch logic)
        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<ProduceListing>> GetByCategory(string category)
        {
            var filtered = _listings
                .Where(l => l.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        // Week 1 Feature: GET string summaries
        [HttpGet("summary")]
        public ActionResult<IEnumerable<string>> GetSummaries()
        {
            var summaries = _listings.Select(l => l.GetFormattedSummary()).ToList();
            return Ok(summaries);
        }
    }
}