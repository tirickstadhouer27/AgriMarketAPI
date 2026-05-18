namespace AgriMarketAPI.Models
{
    public class Farmer
    {
        public int FarmerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
        // Nesting our brand new Struct here!
        public FarmerLocation LocationDetail { get; set; }
        public double Rating { get; set; } = 0.0;
        public bool IsVerified { get; set; } = false;
    }
}