namespace The_E_Shop_Prices_Checker.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } // Foreign key to Category
        public decimal Price { get; set; }
    }
}
