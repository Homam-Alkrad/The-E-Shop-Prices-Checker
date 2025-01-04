using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using The_E_Shop_Prices_Checker.Models.ViewModels;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;

namespace The_E_Shop_Prices_Checker.Controllers
{
    public class ProductController : Controller
    {
        private readonly E_ShopContext _context;

        public ProductController(E_ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    Price=model.Price,
                    ApplicationUserId = userId
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return Json(new { success = true, message = "Product created successfully." });
            }

            return Json(new { success = false, message = "Invalid data." });
        }

    }
}
