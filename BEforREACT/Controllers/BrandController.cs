using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandServices _brandServices;

        public BrandController(BrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var brands = await _brandServices.GetAllBrands();
            return Ok(
               new
               {
                   data = brands,
                   status = "success"
               });
        }

        // GET: api/Brand/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var brand = await _brandServices.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        // POST: api/Brand
        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand([FromBody] BrandDTO brandDTO)
        {
            if (brandDTO == null || string.IsNullOrEmpty(brandDTO.BrandName))
            {
                return BadRequest("Brand data is invalid.");
            }

            try
            {
                var result = await Task.Run(() => _brandServices.AddBrand(brandDTO));

                if (result)
                {
                    return Ok(new { message = "Brand created successfully." });
                }
                else
                {
                    return StatusCode(500, "Failed to create Brand.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // PUT: api/Brand/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] BrandDTO brandDTO)
        {
            try
            {
                var updatedBrand = await _brandServices.UpdateBrand(id, brandDTO);
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
            var result = await _brandServices.DeleteBrand(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
