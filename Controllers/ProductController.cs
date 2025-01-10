using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using The_E_Shop_Prices_Checker.Models.ViewModels;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;
using Microsoft.AspNetCore.Authorization;

namespace The_E_Shop_Prices_Checker.Controllers
{
    [Authorize]
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
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.Price,
                    description = p.Description,
                    categoryId = p.CategoryId
                })
                .FirstOrDefault();

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            return Json(product);
        }

        [HttpPost]
        public IActionResult EditProduct([FromBody] EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == model.Id);

                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found." });
                }

                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;

                _context.Products.Update(product);
                _context.SaveChanges();

                return Json(new { success = true, message = "Product updated successfully." });
            }

            return Json(new { success = false, message = "Invalid data." });
        }

    }
}
