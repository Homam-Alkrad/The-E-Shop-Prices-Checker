using Microsoft.AspNetCore.Mvc;
using System.Linq;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;

namespace The_E_Shop_Prices_Checker.Controllers
{
    public class SystemStatisticsController : Controller
    {
        private readonly E_ShopContext _context;

        public SystemStatisticsController(E_ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStatistics()
        {
            // Count users with the User role
            var totalUsers = _context.Users.Count(u => !u.IsAdmin);

            // Count users with the Admin role
            var totalCompanies = _context.Users.Count(u => u.IsAdmin);

            // Count total products
            var totalProducts = _context.Products.Count();

            return Json(new
            {
                totalUsers,
                totalCompanies,
                totalProducts
            });
        }
    }
}
