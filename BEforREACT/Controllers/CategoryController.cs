using BEforREACT.DTOs;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryServices _categoryServices;

    public CategoryController(CategoryServices categoryService)
    {
        _categoryServices = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoryServices.GetAllCategories();
        return Ok(
            new
            {
                data = categories,
                status = "succes"

            }
            );
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _categoryServices.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null || string.IsNullOrEmpty(categoryDTO.CategoryName))
        {
            return BadRequest("Category data is invalid.");
        }

        try
        {
            var result = await Task.Run(() => _categoryServices.AddCategory(categoryDTO));

            if (result)
            {
                return Ok(new { message = "Category created successfully." });
            }
            else
            {
                return StatusCode(500, "Failed to create category.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }


    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            var updatedBrand = await _categoryServices.UpdateCategory(id, categoryDTO);
            return Ok(updatedBrand);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _categoryServices.DeleteCategory(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }



}
