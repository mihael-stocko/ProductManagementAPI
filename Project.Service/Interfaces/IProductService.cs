using Project.Service.Dtos;
using Project.Service.Models;

namespace Project.Service.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isActive, bool? inStock, string? sortBy, int page, int pageSize);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<bool> UpdateProductAsync(int id, CreateProductDto dto);
    Task<bool> DeleteProductAsync(int id);

    Task<List<CategoryDto>> GetCategoriesAsync(string? searchTerm, string? sortBy, int page, int pageSize);
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
    Task<bool> UpdateCategoryAsync(int id, CreateCategoryDto dto);
    Task<bool> DeleteCategoryAsync(int id);
}