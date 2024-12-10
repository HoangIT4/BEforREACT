using BEforREACT.DTOs;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productServices.GetAllProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(
               new { data = products }
                );
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetProductItems([FromQuery] ProductQuerryParams productParams)
        {
            try
            {
                var products = await _productServices.GetProductItemsAsync(productParams);
                return Ok(
                   new
                   {
                       status = "success",
                       message = "get all products successfully",
                       data = products
                   });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetail(Guid productId)
        {
            try
            {
                var productDetail = await _productServices.GetProductDetailAsync(productId);

                if (productDetail == null)
                {
                    return NotFound($"Product with ID {productId} not found");
                }

                return Ok(productDetail);  // Trả về chi tiết sản phẩm dưới dạng JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] ProductCreateRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest(new { message = "Thông tin sản phẩm không hợp lệ." });
            }

            try
            {
                var result = _productServices.AddProduct(request);

                if (result)
                {
                    return Ok(new { message = "Thêm sản phẩm thành công." });
                }
                else
                {
                    return StatusCode(500, new { message = "Đã xảy ra lỗi khi thêm sản phẩm." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPatch("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, ProductsDetailDTO request)
        {
            var result = await _productServices.UpdateProductAsync(productId, request);
            if (result)
                return Ok(new { message = "Product updated successfully" });
            else
                return NotFound(new { message = "Product not found" });
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var result = await _productServices.DeleteProductAsync(productId);

            if (result)
                return Ok(new { message = "Product deleted successfully" });
            else
                return NotFound(new { message = "Product not found" });
        }
    }
}
