using BEforREACT.Data.Entities;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryServices _categoryService;

    public CategoryController(CategoryServices categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        return Ok(categories);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        var createdCategory = await _categoryService.CreateCategory(category);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryID }, createdCategory);
    }


}
