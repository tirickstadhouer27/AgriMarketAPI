using Microsoft.AspNetCore.Mvc;
using AgriMarketAPI.Models;
using System.Collections.Generic;

namespace AgriMarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private static List<Farmer> _farmers = new List<Farmer>();

        // TC-11 / FR-03.1: POST /api/farmers
        [HttpPost]
        public IActionResult RegisterFarmer([FromBody] Farmer newFarmer)
        {
            if (string.IsNullOrWhiteSpace(newFarmer.Email) || !newFarmer.Email.Contains("@"))
            {
                return BadRequest(new { message = "A valid email address is required." });
            }

            newFarmer.FarmerId = _farmers.Count + 1;
            _farmers.Add(newFarmer);

            return CreatedAtRoute(new { id = newFarmer.FarmerId }, newFarmer);
        }
    }
}