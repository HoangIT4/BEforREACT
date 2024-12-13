using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class OrderServices
    {
        private readonly DataContext _context;
        public OrderServices(DataContext context)
        {
            _context = context;

        }

        public async Task<List<OrderDetailRes>> GetAllOrders()
        {
            var orderDetails = await _context.Orders
                .Where(order => order.DeleteAt == null)
                .Select(order => new OrderDetailRes
                {
                    OrderID = order.OrderID,
                    UserID = order.UserID,
                    Fullname = order.Fullname,
                    TotalPrice = order.OrderItems.Sum(oi => oi.TotalPrice ?? 0),
                    Status = order.Status ?? 0,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    PaymentMethod = order.PaymentMethod,
                    CreateAt = order.CreateAt ?? DateTime.UtcNow,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDetailRes
                    {
                        OrderItemID = oi.OrderItemID,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity ?? 0,
                        Price = oi.Price ?? 0,
                        TotalPrice = oi.TotalPrice ?? 0,
                        ProductName = _context.Products
                            .Where(p => p.ProductID == oi.ProductId)
                            .Select(p => p.Name)
                            .FirstOrDefault()
                    }).ToList()
                })
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();

            return orderDetails;
        }


        public async Task<Order> CreateOrderFromCart(Guid userId, string fullname,string address, string phoneNumber, string paymentMethod)
        {
            // Lấy danh sách sản phẩm từ bảng Cart của người dùng
            var cartItems = await _context.Carts
                                           .Where(c => c.UserID == userId)
                                           .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                throw new Exception("Giỏ hàng của bạn đang trống.");

            // Tạo một đối tượng Order mới
            var newOrder = new Order
            {
                OrderID = Guid.NewGuid(),
                UserID = userId,
                Address = address,
                PhoneNumber = phoneNumber,
                PaymentMethod = paymentMethod,
                Status = 0, // Trạng thái ban đầu là Pending
                CreateAt = DateTime.UtcNow
            };

            // Sử dụng transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Thêm Order vào DbSet
                    _context.Orders.Add(newOrder);

                    foreach (var cartItem in cartItems)
                    {
                        // Lấy thông tin sản phẩm từ bảng Products
                        var product = await _context.Products
                            .Include(p => p.Detail) // Bao gồm thông tin chi tiết của sản phẩm
                            .FirstOrDefaultAsync(p => p.ProductID == cartItem.ProductID);

                        if (product == null)
                            throw new Exception($"Sản phẩm với ID {cartItem.ProductID} không tồn tại.");

                        var orderItem = new OrderItem
                        {
                            OrderItemID = Guid.NewGuid(),
                            OrderID = newOrder.OrderID,
                            ProductId = cartItem.ProductID,
                            Quantity = cartItem.Quantity,
                            Price = product.Detail.Price,
                            TotalPrice = cartItem.Quantity * product.Detail.Price,
                        };

                        // Thêm OrderItem vào DbSet
                        _context.OrderItems.Add(orderItem);
                    }

                    // Xóa tất cả sản phẩm khỏi Cart sau khi đã đặt hàng
                    _context.Carts.RemoveRange(cartItems);

                    // Lưu thông tin vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    return newOrder;
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    await transaction.RollbackAsync();
                    // Ném lại lỗi
                    throw new Exception($"Có lỗi xảy ra khi tạo đơn hàng: {ex.Message}");
                }
            }
        }

        public async Task<Order> UpdateOrderStatus(Guid orderId, int newStatus)
        {
            //0: khoi tao, 1 : da duyet, 2 huy
            //ifneewwStatus = 0) => <p>khoi tao</p>
            // Tìm đơn hàng theo OrderID
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception($"Đơn hàng với ID {orderId} không tồn tại.");

            // Cập nhật trạng thái
            order.Status = newStatus;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return order;
        }
        public async Task<List<OrderDetailRes>> GetOrdersByUserID(Guid userID)
        {
            var orderDetails = await _context.Orders
                .Where(order => order.UserID == userID && order.DeleteAt == null)
                .Select(order => new OrderDetailRes
                {
                    OrderID = order.OrderID,
                    TotalPrice = order.OrderItems.Sum(oi => oi.TotalPrice ?? 0),
                    Status = order.Status ?? 0,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    PaymentMethod = order.PaymentMethod,
                    CreateAt = order.CreateAt ?? DateTime.UtcNow,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDetailRes
                    {
                        OrderItemID = oi.OrderItemID,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity ?? 0,
                        Price = oi.Price ?? 0,
                        TotalPrice = oi.TotalPrice ?? 0,
                        ProductName = _context.Products
                        .Where(p => p.ProductID == oi.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault() // Lấy tên sản phẩm
                    }).ToList()
                })
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();

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
