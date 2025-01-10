namespace The_E_Shop_Prices_Checker.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign key to Products table
        public int EntityId { get; set; }  // Foreign key to Entities table
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; } // Price specific to the entity
        public bool IsAvailable { get; set; }

        // Navigation properties
        public Product Product { get; set; }
    }
}
