using Microsoft.EntityFrameworkCore;
using RestuarantOrderSystem.Models;
using System;

namespace RestuarantOrderSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<RestaurantInfo> RestaurantInfos { get; set; }
        public DbSet<RestaurantTable> Tables { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SystemAnnouncement> SystemAnnouncements { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RestaurantInfo>().HasData(
                new RestaurantInfo { Id = 1, RestaurantName = "Gourmet Bistro RROMS", Address = "123 Culinary Avenue", ContactNumber = "+1-800-555-RROMS" }
            );

            modelBuilder.Entity<MenuCategory>().HasData(
                new MenuCategory { CategoryId = 1, Name = "Starters" },
                new MenuCategory { CategoryId = 2, Name = "Main Course" },
                new MenuCategory { CategoryId = 3, Name = "Desserts" },
                new MenuCategory { CategoryId = 4, Name = "Beverages" }
            );

            modelBuilder.Entity<RestaurantTable>().HasData(
                new RestaurantTable { TableId = 1, TableNumber = "T-01", Capacity = 2, DiningArea = "Main Hall", Status = TableStatus.Available },
                new RestaurantTable { TableId = 2, TableNumber = "T-02", Capacity = 4, DiningArea = "Main Hall", Status = TableStatus.Available },
                new RestaurantTable { TableId = 3, TableNumber = "T-03", Capacity = 6, DiningArea = "VIP Lounge", Status = TableStatus.Available },
                new RestaurantTable { TableId = 4, TableNumber = "T-04", Capacity = 4, DiningArea = "Outdoor Terrace", Status = TableStatus.Available }
            );

            // Fixed static timestamp for database seeding to prevent EF Core migration errors
            var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    UserId = 1,
                    FullName = "System Administrator",
                    Email = "admin@rroms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    PhoneNumber = "1234567890",
                    Role = UserRole.Admin,
                    CreatedAt = seedDate
                },
                new ApplicationUser
                {
                    UserId = 2,
                    FullName = "Chef Gordon",
                    Email = "staff@rroms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff123!"),
                    PhoneNumber = "0987654321",
                    Role = UserRole.Staff,
                    CreatedAt = seedDate
                },
                new ApplicationUser
                {
                    UserId = 3,
                    FullName = "Alex Johnson",
                    Email = "customer@rroms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer123!"),
                    PhoneNumber = "5551234567",
                    Role = UserRole.Customer,
                    CreatedAt = seedDate
                }
            );
        }
    }
}