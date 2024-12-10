using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;

namespace BEforREACT.Services
{
    public class OrderServices
    {
        private readonly DataContext _context;
        public OrderServices(DataContext context)
        {
            _context = context;

        }

        public async Task<Order> CreateOrder(Guid userId, string address, string phoneNumber, string paymentMethod, List<OrderItem> items)
        {
            var newOrder = new Order
            {
                OrderID = Guid.NewGuid(),
                UserID = userId,
                Address = address,
                PhoneNumber = phoneNumber,
                PaymentMethod = paymentMethod,
                Status = 0, // Pending
                CreateAt = DateTime.UtcNow,
            };

            _context.Orders.Add(newOrder);

            foreach (var item in items)
            {
                item.OrderItemID = Guid.NewGuid();
                item.OrderID = newOrder.OrderID; // Gắn OrderID vào OrderItem
                _context.OrderItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return newOrder;
        }

        public async Task<List<OrderDetailRes>> GetOrdersByUserID(Guid userID)
        {
            var orderDetails = (
              from order in _context.Orders
              join orderItem in _context.OrderItems on order.OrderID equals orderItem.OrderID
              //join detail in _context.ProductDetails on product.ProductID equals detail.ProductID
              where order.UserID == userID && order.DeleteAt == null
              select new OrderDetailRes
              {
                  OrderID = order.OrderID,
                  OrderItemID = orderItem.OrderItemID,
                  TotalPrice = orderItem.TotalPrice,
                  Status = order.Status,
                  Address = order.Address,
                  PhoneNumber = order.PhoneNumber,
                  PaymentMethod = order.PaymentMethod,
                  CreateAt = order.CreateAt
              })
        .OrderByDescending(x => x.OrderID)
        .ToList();

            return orderDetails;
        }


        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }


        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, int status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa Order
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.DeleteAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
