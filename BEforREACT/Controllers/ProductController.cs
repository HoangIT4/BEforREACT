using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        //[HttpGet("all")]
        //[Authorize]
        //    public async Task<ActionResult<List<Product>>> GetAllProducts()
        //    {
        //        try
        //        {
        //            var productsItem = await _productServices.GetAllProducts();
        //            return Ok(new
        //            {
        //                status = "success",
        //                message = "get all products successfully",
        //                data = productsItem
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new
        //            {
        //                status = "error",
        //                message = ex.Message
        //            });
        //        }
        //    }

        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Product>> GetProductById(Guid id)
        //    {
        //        var product = await _productServices.GetProductById(id);
        //        if (product == null)
        //            return NotFound();
        //        return Ok(product);
        //    }


        //    [HttpGet("category/{categoryId}")]
        //    public async Task<ActionResult<List<Product>>> GetProductsByCategory(Guid categoryId)
        //    {
        //        var products = await _productServices.GetProductsByCategory(categoryId);
        //        return Ok(products);
        //    }


        //    [HttpGet("brand/{brandId}")]
        //    public async Task<ActionResult<List<Product>>> GetProductsByBrand(Guid brandId)
        //    {
        //        var products = await _productServices.GetProductsByBrand(brandId);
        //        return Ok(products);
        //    }
        //    [HttpPost]
        //    public async Task<ActionResult<Product>> AddProduct(Product product)
        //    {
        //        var newProduct = await _productServices.AddProductAsync(product);
        //        return CreatedAtAction(nameof(GetProductById), new { status = "success", id = newProduct.ProductID }, newProduct);
        //    }

        //    [HttpPut("{id}")]
        //    public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
        //    {
        //        var updatedProduct = await _productServices.UpdateProductAsync(id, product);
        //        if (updatedProduct == null)
        //            return NotFound();
        //        return Ok(updatedProduct);
        //    }

        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult> DeleteProduct(Guid id)
        //    {
        //        var result = await _productServices.DeleteProductAsync(id);
        //        if (!result)
        //            return NotFound();
        //        return NoContent();
        //    }

        //}
    }
}
