using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;

namespace The_E_Shop_Prices_Checker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly E_ShopContext _context;
        public HomeController(ILogger<HomeController> logger, E_ShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Start()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                // If the model is valid, save the contact details to the database
                var contact = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNo = model.PhoneNo,
                    Message = model.Message
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                // Optionally, you can redirect to a confirmation page or display a success message
                TempData["Message"] = "Your message has been sent successfully!";
                return RedirectToAction("Contact");
            }

            // If the form is invalid, return the same view with validation errors
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
