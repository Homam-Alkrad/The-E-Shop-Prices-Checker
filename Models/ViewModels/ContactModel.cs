using System.ComponentModel.DataAnnotations;

namespace The_E_Shop_Prices_Checker.Models.ViewModels
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNo { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
