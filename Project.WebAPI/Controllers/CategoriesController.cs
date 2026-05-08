using Microsoft.AspNetCore.Mvc;
using Project.Service.Dtos;
using Project.Service.Interfaces;

namespace Project.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories(
        [FromQuery] string? searchTerm, [FromQuery] string? sortBy,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var categories = await productService.GetCategoriesAsync(searchTerm, sortBy, page, pageSize);
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await productService.GetCategoryByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        var category = await productService.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
    {
        var success = await productService.UpdateCategoryAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var success = await productService.DeleteCategoryAsync(id);
        return success ? NoContent() : NotFound();
    }
}