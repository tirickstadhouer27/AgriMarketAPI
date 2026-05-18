using AgriMarketAPI.Models.Enums;

namespace AgriMarketAPI.Models
{
    // Ch. 8: Structs
    public struct FarmerLocation
    {
        public string FarmName { get; set; }
        public string Town { get; set; }
        public Province Province { get; set; }

        public FarmerLocation(string farmName, string town, Province province)
        {
            FarmName = farmName;
            Town = town;
            Province = province;
        }
    }
}