using Microsoft.AspNetCore.Identity;

namespace The_E_Shop_Prices_Checker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public UserType? Type { get; set; } // Enum for Type
        public string? Address { get; set; }
        public string? ContactInfo { get; set; } // Phone number validation can be handled elsewhere if required

        // Navigation property to Products
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
