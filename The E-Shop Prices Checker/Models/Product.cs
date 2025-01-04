namespace The_E_Shop_Prices_Checker.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } // Foreign key to Category
        public decimal Price { get; set; }
        public Category Category { get; set; } // Navigation property

        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } // Navigation property
    }
}
