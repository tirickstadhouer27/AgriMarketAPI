using System;
using System.Collections.Generic;
using System.Linq;
using AgriMarketAPI.Models;
using AgriMarketAPI.Models.Enums;
using AgriMarketAPI.Exceptions;

namespace AgriMarketAPI.Repositories
{
    public class ProduceRepository
    {
        private static List<ProduceListing> _listings = new List<ProduceListing>
        {
            new ProduceListing(1, "Potatoes", Category.Vegetables, 15.50, 100, true),
            new ProduceListing(2, "Tomatoes", Category.Vegetables, 20.00, 50, false),
            new ProduceListing(3, "Apples", Category.Fruit, 25.00, 200, true)
        };

        public IEnumerable<ProduceListing> GetAll() => _listings;
        public IEnumerable<ProduceListing> GetAvailable() => _listings.Where(l => l.IsAvailable);
        public IEnumerable<ProduceListing> GetByCategory(Category category) => _listings.Where(l => l.Category == category);

        public ProduceListing GetById(int id)
        {
            var listing = _listings.FirstOrDefault(l => l.ListingId == id);
            if (listing == null)
            {
                throw new ListingNotFoundException(id);
            }
            return listing;
        }

        public void Add(ProduceListing listing)
        {
            int nextId = _listings.Count > 0 ? _listings.Max(l => l.ListingId) + 1 : 1;
            listing.ListingId = nextId;
            _listings.Add(listing);
        }
    }
}