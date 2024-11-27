using BEforREACT.Data.Entities;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandServices _brandService;

        public BrandController(BrandServices brandService)
        {
            _brandService = brandService;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var brands = await _brandService.GetAllBrands();
            return Ok(brands);
        }

        // GET: api/Brand/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var brand = await _brandService.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        // POST: api/Brand
        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
        {
            var createdBrand = await _brandService.CreateBrand(brand);
            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.BrandID }, createdBrand);
        }

        // PUT: api/Brand/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, Brand brand)
        {
            try
            {
                var updatedBrand = await _brandService.UpdateBrand(id, brand);
                return Ok(updatedBrand);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Brand/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var result = await _brandService.DeleteBrand(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
