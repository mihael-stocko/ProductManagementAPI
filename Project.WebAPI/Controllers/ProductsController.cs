using Microsoft.AspNetCore.Mvc;
using Project.Service.Dtos;
using Project.Service.Interfaces;

namespace Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts(
        [FromQuery] int? categoryId, [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice, [FromQuery] bool? isActive,
        [FromQuery] bool? inStock, [FromQuery] string? sortBy,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var products = await productService.GetProductsAsync(categoryId, minPrice, maxPrice, isActive, inStock, sortBy, page, pageSize);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
    {
        var product = await productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, CreateProductDto dto)
    {
        var success = await productService.UpdateProductAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await productService.DeleteProductAsync(id);
        return success ? NoContent() : NotFound();
    }
}