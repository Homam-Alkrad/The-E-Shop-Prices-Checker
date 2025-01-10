using System.ComponentModel.DataAnnotations;

namespace The_E_Shop_Prices_Checker.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [MinLength(3, ErrorMessage = "Full Name must be at least 3 characters long.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; } = false;

        [Display(Name = "Type")]
        public UserType? Type { get; set; } // Enum for Type

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Contact Info")]
        public string? ContactInfo { get; set; }
    }

    public enum UserType
    {
        Owner,
        Company,
        Mall,
        Supermarket
    }
}
