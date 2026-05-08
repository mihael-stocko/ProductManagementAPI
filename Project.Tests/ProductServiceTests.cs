using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Services;
using SQLitePCL;
using Xunit.Sdk;

namespace Project.Tests;

public class ProductServiceTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetProductsAsync_FiltersByPrice()
    {
        var context = GetDbContext();
        var category = new ProductCategory { Id = 1, Name = "Test" };
        context.Categories.Add(category);
        context.Products.AddRange(
            new Product{ Name = "Cheap", Price = 10, CategoryId = 1, StockQuantity = 10 },
            new Product{ Name = "Expensive", Price = 100, CategoryId = 1, StockQuantity = 10 }
        );
        await context.SaveChangesAsync();

        var service = new ProductService(context);

        var result = await service.GetProductsAsync(null, null, 50, null, null, null, 1, 10);

        Assert.Single(result);
        Assert.Equal("Cheap", result[0].Name);
    }

    [Fact]
    public async Task GetProductsAsync_PagingWorks()
    {
        var context = GetDbContext();
        context.Categories.Add(new ProductCategory { Id = 1, Name = "Test" });
        for(int i = 1; i <= 15; i++)
        {
            context.Products.Add(new Product { Name = $"Product {i}", CategoryId = 1 });
        }
        await context.SaveChangesAsync();

        var service = new ProductService(context);

        var result = await service.GetProductsAsync(null, null, null, null, null, null, 2, 10);

        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull_WhenNotFound()
    {
        var context = GetDbContext();
        var service = new ProductService(context);

        var result = await service.GetProductByIdAsync(999);

        Assert.Null(result);
    }
}