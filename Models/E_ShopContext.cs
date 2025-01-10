using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace The_E_Shop_Prices_Checker.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace The_E_Shop_Prices_Checker
    {
        public class E_ShopContext : IdentityDbContext<ApplicationUser>
        {
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }

            public E_ShopContext(DbContextOptions<E_ShopContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder); // Important for configuring Identity-related tables

                // Configure Product-Category relationship
                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade); // Optional: Allows cascading delete for this relationship

                // Configure Product-ApplicationUser relationship
                modelBuilder.Entity<Product>()
                    .HasOne(p => p.ApplicationUser)
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for this relationship
            }
        }
    }

}
