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
        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> AddToCart([FromBody] Cart request)
        //{
        //    try
        //    {

        //        await _cartServices.AddToCart(request.UserID, request.ProductID, request.Quantity);
        //        return Ok(new { message = "Product added to cart successfully" });
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
    }
}
