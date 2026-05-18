using System;
using System.Collections.Generic;
using System.Linq;
using AgriMarketAPI.Models;
using AgriMarketAPI.Models.Enums;
using AgriMarketAPI.Exceptions;
using AgriMarketAPI.Interfaces;

namespace AgriMarketAPI.Repositories
{
    // Ch. 9: Implementing the strict generic repository contract for ProduceListing
    public class ProduceRepository : IRepository<ProduceListing>
    {
        // Concrete in-memory data store for Week 3 operations
        private static List<ProduceListing> _listings = new List<ProduceListing>
        {
            new ProduceListing(1, "Potatoes", Category.Vegetables, 15.50, 100, true),
            new ProduceListing(2, "Tomatoes", Category.Vegetables, 20.00, 50, false),
            new ProduceListing(3, "Apples", Category.Fruit, 25.00, 200, true)
        };

        // TC-01 / FR-01.4: Contract requirement - Get all items
        public IEnumerable<ProduceListing> GetAll() => _listings;

        // TC-04 / FR-01.4: Custom domain method - Get only available items
        public IEnumerable<ProduceListing> GetAvailable() => _listings.Where(l => l.IsAvailable);

        // TC-05 / FR-01.5: Custom domain method - Get items by Enum category
        public IEnumerable<ProduceListing> GetByCategory(Category category) => _listings.Where(l => l.Category == category);

        // TC-06 & TC-07: Contract requirement - Fetch by ID with custom Exception Handling
        // Note the '?' which aligns perfectly with your updated IRepository interface definition
        public ProduceListing? GetById(int id)
        {
            var listing = _listings.FirstOrDefault(l => l.ListingId == id);
            
            // Ch. 7: Exception logic guard clause
            if (listing == null)
            {
                throw new ListingNotFoundException(id);
            }
            return listing;
        }

        // TC-03 / FR-01.1: Contract requirement - Add a new item to the data collection
        public void Add(ProduceListing listing)
        {
            // Auto-assign ID incrementally based on highest available record key
            int nextId = _listings.Count > 0 ? _listings.Max(l => l.ListingId) + 1 : 1;
            listing.ListingId = nextId;
            _listings.Add(listing);
        }

        // Contract requirement - Delete an item from the collection by ID
        public void Delete(int id)
        {
            // Reuses GetById logic so it throws a ListingNotFoundException automatically if ID doesn't exist
            var listing = GetById(id);
            if (listing != null)
            {
                _listings.Remove(listing);
            }
        }
    }
}