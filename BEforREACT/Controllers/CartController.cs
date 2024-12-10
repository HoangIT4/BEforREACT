using BEforREACT.DTOs;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartServices _cartServices;
        public CartController(CartServices cartServices)
        {
            _cartServices = cartServices;
        }
        // Lấy giỏ hàng của người dùng
        [HttpGet("{UserID}")]
        public IActionResult GetCartByUserId(Guid UserID)
        {
            //find
            var cartItems = _cartServices.GetCartByUserId(UserID);



            return Ok(
                new { data = cartItems });
        }

        // Thêm sản phẩm vào giỏ
        [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            // Kiểm tra các giá trị từ request (có thể tạo một lớp DTO cho request này)
            if (request.Quantity <= 0)
            {
                return BadRequest("Số lượng sản phẩm không hợp lệ.");
            }

            var result = await _cartServices.AddToCart(request);

            return Ok(new
            {
                message = "Add Product Sucessfuly ",
                status = "success",
                data = result
            });

        }

        // Cập nhật số lượng sản phẩm trong giỏ
        [HttpPatch("{cartID}")]
        public async Task<IActionResult> UpdateCartItem(Guid cartID, [FromBody] UpdateCartRequest request)
        {
            if (request.Quantity <= 0)
            {
                return BadRequest("Số lượng sản phẩm phải lớn hơn 0.");
            }

            var result = await _cartServices.UpdateCartItem(cartID, request.Quantity);

            if (result)
            {
                return Ok(
                    new
                    {
                        data = result,
                        message = "Sản phẩm đã được cập nhật."
                    });
            }

            return NotFound("Không tìm thấy sản phẩm trong giỏ.");
        }



        [HttpDelete("ClearAllCart")]
        public async Task<IActionResult> ClearCart([FromBody] ClearCart request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cartServices.ClearCart(request.UserID);
            if (result)
            {
                return Ok(
                    new
                    {
                        data = result,
                        Message = "Cart cleared successfully!"
                    });
            }

            return NotFound(new { Message = "No items to clear in the cart." });
        }


        [HttpDelete("DeleteCart-Item")]
        public async Task<IActionResult> DeleteCartItem([FromBody] DeleteCartItem request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cartServices.DeleteCartItem(request);
            if (result)
            {
                return Ok(new { Message = "CartItem cleared successfully!" });
            }

            return NotFound(new { Message = "No items to clear in the cart." });
        }
    }
}

