using BEforREACT.Data.Entities;
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

        // Lấy danh sách Order
        [HttpGet]
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
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Guid userId, string address, string phoneNumber, string paymentMethod, [FromBody] List<OrderItem> items)
        {
            var newOrder = await _orderService.CreateOrder(userId, address, phoneNumber, paymentMethod, items);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderID }, newOrder);
        }

        // Cập nhật trạng thái Order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] int status)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, status);
            if (!success) return NotFound("Order không tồn tại.");
            return NoContent();
        }

        // Xóa Order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var success = await _orderService.DeleteOrderAsync(id);
            if (!success) return NotFound("Order không tồn tại.");
            return NoContent();
        }
    }

}
