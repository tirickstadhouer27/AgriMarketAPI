using AgriMarketAPI.Models.Enums;

namespace AgriMarketAPI.Models
{
    // Buyer inherits from Person
    public class Buyer : Person
    {
        public BuyerType BuyerType { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;

        // Polymorphic Override (TC-16)
        public override string GetContactInfo()
        {
            return $"[{BuyerType} Buyer] {FullName} - Address: {DeliveryAddress}";
        }
    }
}