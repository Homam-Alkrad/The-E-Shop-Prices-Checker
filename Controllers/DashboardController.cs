using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;
using The_E_Shop_Prices_Checker.Models.ViewModels;

namespace The_E_Shop_Prices_Checker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly E_ShopContext _context;

        public DashboardController(E_ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Fetch distinct products by name and sort by price
            var products = _context.Products
                .AsEnumerable() // Move query to LINQ-to-Objects for grouping
                .GroupBy(p => p.Name) // Group by Name to ensure unique product names
                .Select(g => g.OrderBy(p => p.Price).First()) // Select the product with the lowest price in each group
                .OrderBy(p => p.Price) // Sort the final list by price
                .ToList();

            return View(products);
        }



        [HttpGet]
        public async Task<IActionResult> GetProductData(string productName)
        {
            var productData = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ApplicationUser)
                .Where(p => p.Name == productName)
                .Select(p => new
                {
                    ProductName = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    IsAvailable = p.ApplicationUser != null,
                    OwnerName = p.ApplicationUser.FullName
                })
                .ToListAsync();

            return Json(productData);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductPriceRange(string productName)
        {
            var productPrices = await _context.Products
                .Include(p => p.ApplicationUser) // Include owner details
                .Where(p => p.Name == productName) // Filter by product name
                .Select(p => new
                {
                    Price = p.Price,
                    OwnerName = p.ApplicationUser.FullName,
                    OwnerType = p.ApplicationUser.Type.ToString(),
                    OwnerAddress = p.ApplicationUser.Address,
                    OwnerContact = p.ApplicationUser.ContactInfo
                })
                .ToListAsync();

            if (productPrices.Any())
            {
                var maxPrice = productPrices.OrderByDescending(p => p.Price).First();
                var minPrice = productPrices.OrderBy(p => p.Price).First();

                return Json(new
                {
                    maxPrice = maxPrice.Price,
                    maxOwner = maxPrice,
                    minPrice = minPrice.Price,
                    minOwner = minPrice
                });
            }

            return Json(new
            {
                maxPrice = 0,
                maxOwner = new { OwnerName = "N/A", OwnerType = "N/A", OwnerAddress = "N/A", OwnerContact = "N/A" },
                minPrice = 0,
                minOwner = new { OwnerName = "N/A", OwnerType = "N/A", OwnerAddress = "N/A", OwnerContact = "N/A" }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemStats()
        {
            var stats = new
            {
                UsersCount = await _context.Users.CountAsync(),
                ProductsCount = await _context.Products.CountAsync(),
                CategoriesCount = await _context.Categories.CountAsync()
            };

            return Json(stats);
        }


        public IActionResult Companies()
        {
            // Return all users with IsAdmin set to true
            var adminUsers = _context.Users.Where(u => u.IsAdmin).ToList();
            return View(adminUsers);
        }

        public IActionResult Products()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ApplicationUser)
                .ToList();

            var maxPriceProduct = products.OrderByDescending(p => p.Price).FirstOrDefault();
            var minPriceProduct = products.OrderBy(p => p.Price).FirstOrDefault();

            var viewModel = new UserProductsViewModel
            {
                Products = products,
                MaxPriceProduct = maxPriceProduct,
                MinPriceProduct = minPriceProduct,
                TotalProducts = products.Count,
                Categories = _context.Categories.ToList() // Assuming Categories exist in your DbContext
            };

            return View(viewModel);
        }


        public IActionResult Users()
        {
            // Return users with detailed information
            var users = _context.Users
                .Select(u => new UserViewModel
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Type = u.Type.HasValue ? u.Type.ToString() : "N/A", // Convert enum to string
                    Address = u.Address ?? "N/A", // Handle null values
                    ContactInfo = u.ContactInfo ?? "N/A",
                    IsBlocked = u.IsBlocked
                })
                .ToList();

            return View(users); // Pass the list to the view
        }

        [HttpGet]
        public IActionResult ProductManagements()
        {
            // Return all products for the logged-in user with statistics
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProducts = _context.Products
                .Where(p => p.ApplicationUserId == userId)
                .Include(p => p.Category)
                .ToList();

            var categories = _context.Categories.ToList();

            if (userProducts.Any())
            {
                var maxPriceProduct = userProducts.OrderByDescending(p => p.CategoryId).FirstOrDefault(); // Assuming CategoryId represents a price placeholder
                var minPriceProduct = userProducts.OrderBy(p => p.CategoryId).FirstOrDefault(); // Assuming CategoryId represents a price placeholder

                var model = new UserProductsViewModel
                {
                    Products = userProducts,
                    MaxPriceProduct = maxPriceProduct,
                    MinPriceProduct = minPriceProduct,
                    TotalProducts = userProducts.Count,
                    Categories = categories
                };

                return View(model);
            }

            return View(new UserProductsViewModel { Categories = categories });
        }

    }
}
