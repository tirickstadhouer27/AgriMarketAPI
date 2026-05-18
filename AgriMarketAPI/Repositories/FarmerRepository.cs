using System.Collections.Generic;
using System.Linq;
using AgriMarketAPI.Models;
using AgriMarketAPI.Interfaces;

namespace AgriMarketAPI.Repositories
{
    // Ch. 9: Reusing our generic repository contract for the Farmer model
    public class FarmerRepository : IRepository<Farmer>
    {
        private static List<Farmer> _farmers = new List<Farmer>();

        // Retrieves all registered farmers
        public IEnumerable<Farmer> GetAll() => _farmers;

        // Retrieves a single farmer by their Id. 
        // The '?' allows it to return null gracefully if no match is found.
        public Farmer? GetById(int id)
        {
            return _farmers.FirstOrDefault(f => f.Id == id);
        }

        // Adds a new farmer and auto-increments the inherited Id field
        public void Add(Farmer farmer)
        {
            farmer.Id = _farmers.Count > 0 ? _farmers.Max(f => f.Id) + 1 : 1;
            _farmers.Add(farmer);
        }

        // Deletes a farmer from the collection by their Id
        public void Delete(int id)
        {
            var farmer = GetById(id);
            if (farmer != null)
            {
                _farmers.Remove(farmer);
            }
        }
    }
}