using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;
using The_E_Shop_Prices_Checker.Models.ViewModels;

namespace The_E_Shop_Prices_Checker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly E_ShopContext _context;

        public AccountController(E_ShopContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email,
                    IsBlocked = false, // Default value
                    Type = (Models.UserType?)model.Type,
                    Address = model.Address,
                    ContactInfo = model.ContactInfo
                };
                if (model.Type != null)
                    model.IsAdmin = true;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = model.IsAdmin ? "Admin" : "User";

                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // Check if the user is blocked
                    if (user.IsBlocked)
                    {
                        ModelState.AddModelError(string.Empty, "Your account has been blocked. Please contact support.");
                        return View(model);
                    }

                    // Attempt sign-in
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Redirect based on user role
                        if (!await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }

                        return RedirectToAction("ProductManagements", "Dashboard");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ToggleBlock(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userId);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Users","Dashboard");
            }

            user.IsBlocked = !user.IsBlocked; // Toggle the IsBlocked status
            _context.Update(user); // Save changes to the database
            _context.SaveChanges();

            TempData["Success"] = user.IsBlocked ? "User blocked successfully." : "User unblocked successfully.";


            return RedirectToAction("Users", "Dashboard");
        }

    }
}
