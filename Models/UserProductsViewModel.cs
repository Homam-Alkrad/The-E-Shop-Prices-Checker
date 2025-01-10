namespace The_E_Shop_Prices_Checker.Models
{
    public class UserProductsViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public Product MaxPriceProduct { get; set; }
        public Product MinPriceProduct { get; set; }
        public int TotalProducts { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
