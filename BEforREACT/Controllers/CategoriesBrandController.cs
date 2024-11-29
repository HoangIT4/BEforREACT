using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesBrandController : ControllerBase
    {
        private CategoriesBrandServices _services;

        public CategoriesBrandController(CategoriesBrandServices services)
        {
            _services = services;
        }

        [HttpGet("brands-by-category/{categoryId}")]
        public async Task<IActionResult> GetBrandsByCategoryId(Guid categoryId)
        {
            var brands = await _services.GetBrandsByCategoryId(categoryId);
            return Ok(brands);
        }

        [HttpGet("categories-by-brand/{brandId}")]
        public async Task<IActionResult> GetCategoriesByBrandId(Guid brandId)
        {
            var categories = await _services.GetCategoriesByBrandId(brandId);
            return Ok(categories);
        }

        [HttpPost("Addlink")]
        public async Task<IActionResult> AddCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var result = await _services.AddCategoryBrandLink(categoryId, brandId);
            if (!result) return BadRequest("Link already exists.");
            return Ok("Link added successfully.");
        }

        [HttpDelete("Deletelink")]
        public async Task<IActionResult> RemoveCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var result = await _services.RemoveCategoryBrandLink(categoryId, brandId);
            if (!result) return NotFound("Link not found.");
            return Ok("Link removed successfully.");
        }
    }

}
