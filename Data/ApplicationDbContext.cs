using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using ReserveBite.Api.Models;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace ReserveBite.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Menu)
                .WithOne(m => m.Restaurant)
                .HasForeignKey<Menu>(m => m.RestaurantId);

            modelBuilder.Entity<Cuisine>()
                .HasData(GenerateCuisines());
        }

        private IEnumerable<Cuisine> GenerateCuisines()
        {
            return new List<Cuisine>()
            {
                new Cuisine()
                {
                    Id = 1,
                    Name = "Italian",
                    ImgUrl = "https://amazingfoodanddrink.com/wp-content/uploads/2024/05/The-Flavors-of-Italian-Street-Food_-259434423.jpg"
                },
                new Cuisine()
                {
                    Id = 2,
                    Name = "Greek",
                    ImgUrl = "https://kavala-online.com/wp-content/uploads/2024/08/greek-food-plate-1024x585.webp"
                },
                new Cuisine()
                {
                    Id = 3,
                    Name = "Bulgarian",
                    ImgUrl = "https://tripjive.com/wp-content/uploads/2024/06/Where-to-eat-traditional-Bulgarian-food-in-Sofia.jpg"
                },
                new Cuisine()
                {
                    Id = 4,
                    Name = "Indian",
                    ImgUrl = "https://www.tastingtable.com/img/gallery/20-delicious-indian-dishes-you-have-to-try-at-least-once/intro-1645057933.jpg"
                },
                new Cuisine()
                {
                    Id = 5,
                    Name = "Mexican",
                    ImgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7PrURqk9v5JSOVaUKkSvFgNsqePcWfebTnQ&s"
                },
            };
        }
    }
}
