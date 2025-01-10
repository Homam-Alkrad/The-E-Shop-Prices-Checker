using Microsoft.AspNetCore.Identity;

namespace The_E_Shop_Prices_Checker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; } = false; // Default value is false
        public UserType? Type { get; set; }
        public string? Address { get; set; }
        public string? ContactInfo { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public enum UserType
    {
        Owner,
        Company,
        Mall,
        Supermarket
    }
}
