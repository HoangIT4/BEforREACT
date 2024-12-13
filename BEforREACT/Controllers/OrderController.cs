using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using BEforREACT.Services;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _orderService;

        // Dependency Injection vào controller
        public OrderController(OrderServices orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Admin")]
        public async Task<ActionResult<List<OrderDetailRes>>> GetAllOrdersForAdmin()
        {

            var orders = await _orderService.GetAllOrders();
            return Ok(orders);


        }


        // Lấy danh sách Order
        [HttpGet("All")]
        public async Task<ActionResult<List<Order>>> GetOrders(Guid UserID)
        {
            var orders = await _orderService.GetOrdersByUserID(UserID);
            return Ok(orders);
        }

        // Lấy Order theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order không tồn tại.");
            return Ok(order);
        }

        // Tạo mới Order
        [HttpPost("create")]
        public async Task<ActionResult<Order>> CreateOrder(Guid userId, string address, string phoneNumber, string paymentMethod)
        {
            var newOrder = await _orderService.CreateOrderFromCart(userId, address, phoneNumber, paymentMethod);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderID }, newOrder);
        }



        // Cập nhật trạng thái Order
        [HttpPut("changeStatus/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] int status)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, status);
            if (!success) Ok(new { status = false, message = "Cập nhật trạng thái thất bại." });
            return Ok(new { status = true, message = "Cập nhật trạng thái thành công." });
        }

        // Xóa Order
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var success = await _orderService.DeleteOrderAsync(id);
            if (!success) return NotFound("Order không tồn tại.");
            return NoContent();
        }
    }

}
