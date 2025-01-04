using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using The_E_Shop_Prices_Checker.Models;
using The_E_Shop_Prices_Checker.Models.The_E_Shop_Prices_Checker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Add Razor Pages support (required for Identity UI)

// Register Razor Pages service
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<E_ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser and IdentityRole
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Customize Identity options as needed
})
    .AddEntityFrameworkStores<E_ShopContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // More detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Handle errors in production
    app.UseHsts(); // Enforce HTTPS for security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add this before UseAuthorization
app.UseAuthorization();

// Map endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Map Razor Pages (required for Identity UI)

// Seed roles and default users
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await SeedRolesAndUsers(roleManager, userManager);
}

app.Run();

async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Define roles
    var roles = new[] { "Admin", "SuperAdmin" };

    // Create roles if they don't already exist
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Create a default SuperAdmin user
    var superAdminEmail = "superadmin@example.com";
    var superAdminPassword = "SuperAdmin@123";

    if (await userManager.FindByEmailAsync(superAdminEmail) == null)
    {
        var superAdmin = new ApplicationUser
        {
            FullName = "Super Admin",
            IsAdmin = true,
            UserName = superAdminEmail,
            Email = superAdminEmail,
            EmailConfirmed = true // Set to true if email confirmation is not required
        };

        var createSuperAdmin = await userManager.CreateAsync(superAdmin, superAdminPassword);
        if (createSuperAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }

    // Create a default Admin user
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new ApplicationUser
        {
            FullName = "Admin User",
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            IsAdmin = false // Set IsAdmin to false
        };

        var createAdmin = await userManager.CreateAsync(admin, adminPassword);
        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
