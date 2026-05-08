using Microsoft.EntityFrameworkCore;
using Project.Service.Models;

namespace Project.Service.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductCategory> Categories => Set<ProductCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { Id = 1, Name = "Electronics", Description = "Gadgets and devices" },
            new ProductCategory { Id = 2, Name = "Clothing", Description = "Apparel and accessories" },
            new ProductCategory { Id = 3, Name = "Home & Garden", Description = "Tools and decor" },
            new ProductCategory { Id = 4, Name = "Sports", Description = "Equipment and gear" }
        );

        modelBuilder.Entity<Product>().HasData(
            // Electronics
            new Product { Id = 1, CategoryId = 1, Name = "iPhone 15", Price = 999.99m, StockQuantity = 50, IsActive = true, CreatedAt = DateTime.Parse("2024-01-01") },
            new Product { Id = 2, CategoryId = 1, Name = "Samsung TV", Price = 599.99m, StockQuantity = 20, IsActive = true, CreatedAt = DateTime.Parse("2024-01-05") },
            new Product { Id = 3, CategoryId = 1, Name = "Sony Headphones", Price = 199.99m, StockQuantity = 0, IsActive = true, CreatedAt = DateTime.Parse("2024-01-10") },
            
            // Clothing
            new Product { Id = 4, CategoryId = 2, Name = "Nike Shoes", Price = 120.00m, StockQuantity = 100, IsActive = true, CreatedAt = DateTime.Parse("2024-02-01") },
            new Product { Id = 5, CategoryId = 2, Name = "Levi's Jeans", Price = 65.50m, StockQuantity = 30, IsActive = true, CreatedAt = DateTime.Parse("2024-02-05") },
            new Product { Id = 6, CategoryId = 2, Name = "Winter Jacket", Price = 150.00m, StockQuantity = 10, IsActive = false, CreatedAt = DateTime.Parse("2024-02-10") },
            
            // Home & Garden
            new Product { Id = 7, CategoryId = 3, Name = "Coffee Maker", Price = 89.99m, StockQuantity = 15, IsActive = true, CreatedAt = DateTime.Parse("2024-03-01") },
            new Product { Id = 8, CategoryId = 3, Name = "Garden Hose", Price = 25.00m, StockQuantity = 200, IsActive = true, CreatedAt = DateTime.Parse("2024-03-05") },
            
            // Sports
            new Product { Id = 9, CategoryId = 4, Name = "Yoga Mat", Price = 30.00m, StockQuantity = 45, IsActive = true, CreatedAt = DateTime.Parse("2024-04-01") },
            new Product { Id = 10, CategoryId = 4, Name = "Dumbbells", Price = 45.99m, StockQuantity = 12, IsActive = true, CreatedAt = DateTime.Parse("2024-04-05") },
            new Product { Id = 11, CategoryId = 4, Name = "Tennis Racket", Price = 110.00m, StockQuantity = 5, IsActive = true, CreatedAt = DateTime.Parse("2024-04-10") },
            new Product { Id = 12, CategoryId = 4, Name = "Running Socks", Price = 15.00m, StockQuantity = 500, IsActive = true, CreatedAt = DateTime.Parse("2024-04-15") }
        );
    }
}