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
        //[Authorize]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var productsItem = await _productServices.GetProduct();
                return Ok(new
                {
                    status = "success",
                    message = "get all products successfully",
                    data = productsItem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductsDTO productDto)
        {
            //var productsDtos = new List<ProductsDTO>
            //{
            //    new ProductsDTO
            //    {
            //        ProductID = Guid.NewGuid(),
            //        DetailID = Guid.NewGuid(),
            //        Name = "Nước tẩy bồn cầu Vim Xanh biển 880ml",
            //        Price = 38.000m,
            //        Src = "https://u-shop.vn/images/thumbs/0015488_dau-tam-goi-clear-men-3-trong-1-active-clean-630g.png",
            //        PreImg = "https://u-shop.vn/images/thumbs/0013455_dau-tam-goi-clear-men-3-trong-1-active-clean-630g.png",
            //        Description = "Tóc Sạch Gàu - Dưỡng ẩm da đầu - Sảng khoái toàn thân",
            //        detailDes = "Mô tả chi tiết về sản phẩm",
            //        Stock = 100,
            //        Rating = 4.5f
            //    },
            //    new ProductsDTO
            //    {
            //        ProductID = Guid.NewGuid(),
            //        DetailID = Guid.NewGuid(),
            //        Name = "Dầu tắm gội Clear Men 3 Trong 1 Active Clean 630g",
            //        Price = 198000,
            //        Src = "https://u-shop.vn/images/thumbs/0015488_dau-tam-goi-clear-men-3-trong-1-active-clean-630g.png",
            //        PreImg = "https://u-shop.vn/images/thumbs/0013455_dau-tam-goi-clear-men-3-trong-1-active-clean-630g.png",
            //        Description = "Tóc Sạch Gàu - Dưỡng ẩm da đầu - Sảng khoái toàn thân",
            //        detailDes = "Mô tả chi tiết về sản phẩm",
            //        Stock = 50,
            //        Rating = 4.0f
            //    }
            //};


            if (productDto == null)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Invalid product data."
                });
            }

            try
            {
                var productsData = await _productServices.CreateProduct(productDto);

                return Ok(new
                {
                    status = "success",
                    data = productsData
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }



    }
}
