using System;
using AgriMarketAPI.Models.Enums; // Import your new enums

namespace AgriMarketAPI.Models
{
    public class ProduceListing
    {
        public int ListingId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        
        // Changed from string to Category Enum
        public Category Category { get; set; } = Category.Other; 
        
        public double PricePerKg { get; set; }
        public double QuantityKg { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime HarvestDate { get; set; }

        public ProduceListing() { }

        // Updated constructor to accept the Category Enum
        public ProduceListing(int id, string name, Category category, double price, double quantity, bool available)
        {
            ListingId = id;
            ProductName = name;
            Category = category;
            PricePerKg = price;
            QuantityKg = quantity;
            IsAvailable = available;
            HarvestDate = DateTime.Now;
        }

        public double CalculateRevenue()
        {
            return PricePerKg * QuantityKg;
        }

        public string GetFormattedSummary()
        {
            // .ToString() automatically outputs "Vegetables" or "Fruit" instead of numbers
            return $"{ProductName} ({Category}) - R{PricePerKg}/kg. Available: {QuantityKg}kg.";
        }
    }
}