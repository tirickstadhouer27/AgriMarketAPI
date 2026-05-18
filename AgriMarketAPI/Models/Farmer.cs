namespace AgriMarketAPI.Models
{
    // Farmer inherits from Person
    public class Farmer : Person
    {
        public FarmerLocation LocationDetail { get; set; }
        public double Rating { get; set; } = 0.0;
        public bool IsVerified { get; set; } = false;

        // Polymorphic Override (TC-16)
        public override string GetContactInfo()
        {
            return $"[Farmer] {FullName} from {LocationDetail.FarmName} - Contact: {PhoneNumber}";
        }
    }
}