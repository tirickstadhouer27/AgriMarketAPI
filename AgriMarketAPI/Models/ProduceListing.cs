using System;

namespace AgriMarketAPI.Models
{
    public class ProduceListing
    {
        // Properties (Ch. 2 & 4)
        public int ListingId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = "General"; // Will become an Enum in Week 2
        public double PricePerKg { get; set; }
        public double QuantityKg { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime HarvestDate { get; set; }

        // Parameterless Constructor - CRITICAL for POST requests to work
        public ProduceListing() { }

        // Parameterized Constructor (Ch. 4)
        public ProduceListing(int id, string name, string category, double price, double quantity, bool available)
        {
            ListingId = id;
            ProductName = name;
            Category = category;
            PricePerKg = price;
            QuantityKg = quantity;
            IsAvailable = available;
            HarvestDate = DateTime.Now;
        }

        // TC-02: CalculateRevenue Method (Ch. 3)
        public double CalculateRevenue()
        {
            return PricePerKg * QuantityKg;
        }

        // Week 1 Feature: GetFormattedSummary Method (Ch. 3)
        public string GetFormattedSummary()
        {
            return $"{ProductName} ({Category}) - R{PricePerKg}/kg. Available: {QuantityKg}kg. Revenue Potential: R{CalculateRevenue()}";
        }
    }
}