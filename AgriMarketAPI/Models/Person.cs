namespace AgriMarketAPI.Models
{
    // Ch. 10: Base Class
    public abstract class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // "virtual" allows derived classes to override this behavior polymorphically
        public virtual string GetContactInfo()
        {
            return $"{FullName} (Email: {Email}, Phone: {PhoneNumber})";
        }
    }
}