using System.ComponentModel.DataAnnotations;

namespace Project.Service.Dtos;

public record ProductDto(
    int Id,
    string Name,
    string CategoryName,
    decimal Price,
    int StockQuantity,
    bool IsActive,
    DateTime CreatedAt
);

public record CreateProductDto(
    [Required][StringLength(100)] string Name,
    [Range(1, int.MaxValue)] int CategoryId,
    [Range(0.01, 10000)] decimal Price,
    [Range(0, 5000)] int StockQuantity,
    bool IsActive = true
);

public record CategoryDto(
    int Id,
    string Name,
    string? Description
);

public record CreateCategoryDto(
    [Required][StringLength(100)] string Name,
    [StringLength(500)] string? Description
);