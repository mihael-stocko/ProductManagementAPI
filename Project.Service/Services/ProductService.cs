using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Dtos;
using Project.Service.Interfaces;
using Project.Service.Models;

namespace Project.Service.Services;

public class ProductService(ApplicationDbContext context) : IProductService
{
    public async Task<List<ProductDto>> GetProductsAsync(
        int? categoryId, decimal? minPrice, decimal? maxPrice, 
        bool? isActive, bool? inStock, 
        string? sortBy, int page, int pageSize)
    {
        var query = context.Products.Include(p => p.Category).AsQueryable();

        //Filtering
        if(categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId);
        if(minPrice.HasValue) query = query.Where(p => p.Price >= minPrice);
        if(maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice);
        if(isActive.HasValue) query = query.Where(p => p.IsActive == isActive);
        if(inStock.HasValue) query = inStock.Value ? query.Where(p => p.StockQuantity > 0) : query.Where(p => p.StockQuantity == 0);

        //Sorting
        query = sortBy?.ToLower() switch
        {
            "price" => query.OrderBy(p => p.Price),
            "name" => query.OrderBy(p => p.Name.ToLower()),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        //Paging
        query = query.Skip((page - 1) * pageSize);
        query = query.Take(pageSize);

        return await query
            .Select(p => new ProductDto(
                p.Id, p.Name, p.Category!.Name, p.Price, p.StockQuantity, p.IsActive, p.CreatedAt
            ))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
               
        return product == null ? null : new ProductDto(product.Id, product.Name, product.Category!.Name, product.Price, product.StockQuantity, product.IsActive, product.CreatedAt);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            IsActive = dto.IsActive
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return await GetProductByIdAsync(product.Id) ?? throw new Exception("Error creating product.");
    }

    public async Task<bool> UpdateProductAsync(int id, CreateProductDto dto)
    {
        var product = await context.Products.FindAsync(id);
        if(product == null) return false;

        product.Name = dto.Name;
        product.CategoryId = dto.CategoryId;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.IsActive = dto.IsActive;
        
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if( product == null) return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(string? searchTerm, string? sortBy, int page, int pageSize)
    {
        var query = context.Categories.AsQueryable();

        //Filtering
        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lowerSearch = searchTerm.ToLower();

            query = query.Where(
                c => c.Name.ToLower().Contains(lowerSearch) || (c.Description != null && c.Description.ToLower().Contains(lowerSearch)));
        }

        //Sorting
        query = sortBy?.ToLower() switch
        {
            "name_desc" => query.OrderByDescending(c => c.Name.ToLower()),
            "name" => query.OrderBy(c => c.Name.ToLower()),
            _ => query.OrderBy(c => c.Id)
        };

        //Paging
        query = query.Skip((page - 1) * pageSize);
        query = query.Take(pageSize);

        return await query.Select(c => new CategoryDto(c.Id, c.Name, c.Description)).ToListAsync();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        return category == null ? null : new CategoryDto(category.Id, category.Name, category.Description);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = new ProductCategory { Name = dto.Name, Description = dto.Description };
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return new CategoryDto(category.Id, category.Name, category.Description);
    }

    public async Task<bool> UpdateCategoryAsync(int id, CreateCategoryDto dto)
    {
        var category = await context.Categories.FindAsync(id);
        if(category == null) return false;

        category.Name = dto.Name;
        category.Description = dto.Description;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if(category == null) return false;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return true;
    }
}