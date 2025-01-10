namespace The_E_Shop_Prices_Checker.Models.ViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }
        public string OwnerName { get; set; }
    }
}
