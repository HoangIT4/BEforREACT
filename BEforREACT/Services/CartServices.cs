using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class CartServices
    {
        private readonly DataContext _context;
        public CartServices(DataContext context)
        {
            _context = context;

        }
        public List<AddToCartRes> GetCartByUserId(Guid userId)
        {

            var cartWithDetails = (
            from cart in _context.Carts
            join product in _context.Products on cart.ProductID equals product.ProductID
            join detail in _context.ProductDetails on product.ProductID equals detail.ProductID
            where cart.UserID == userId && cart.DeleteAt == null
            select new AddToCartRes
            {
                CartID = cart.CartID,
                ProductID = cart.ProductID, // Lấy ProductId từ Cart
                Quantity = cart.Quantity, // Lấy Quantity từ Cart
                UserID = cart.UserID, // Lấy UserId từ Cart
                Name = product.Name, // Lấy Name từ Product
                Src = product.Src, // Lấy Src từ Product
                PreImg = product.PreImg, // Lấy PreImg từ Product
                Total = detail.Price * cart.Quantity,
                Price = detail.Price // Lấy Price từ ProductDetail
            }
        )
        .OrderByDescending(x => x.ProductID) // Sắp xếp theo ProductId
        .ToList();

            return cartWithDetails;
        }




        public async Task<AddToCartRes> AddToCart(AddToCartRequest request)
        {
            // Tìm mục giỏ hàng hiện có
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(x => x.ProductID == request.ProductID && x.UserID == request.UserID && x.DeleteAt == null);

            Guid cartID; // Khai báo biến để lưu CartID

            if (existingCartItem != null)
            {
                // Cập nhật số lượng nếu sản phẩm đã có trong giỏ
                existingCartItem.Quantity += request.Quantity;
                existingCartItem.isMultiple = request.isMultiple;
                cartID = existingCartItem.CartID;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                // Thêm mới sản phẩm vào giỏ
                var cartItem = new Cart
                {
                    CartID = Guid.NewGuid(), // Tạo ID mới cho cart item
                    ProductID = request.ProductID,
                    Quantity = request.Quantity,
                    UserID = request.UserID,
                    isMultiple = request.isMultiple,
                };
                await _context.Carts.AddAsync(cartItem);
                cartID = cartItem.CartID; // Lưu CartID của mục mới
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            var saveResult = await _context.SaveChangesAsync();

            // Kiểm tra xem việc lưu có thành công hay không
            if (saveResult > 0)
            {
                // Lấy thông tin sản phẩm để trả về
                var product = await _context.Products
                    .Include(x => x.Detail) // Include related Detail entity
                    .FirstOrDefaultAsync(p => p.ProductID == request.ProductID); // Use FirstOrDefaultAsync

                // Tạo đối tượng kết quả
                var cartRes = new AddToCartRes()
                {
                    CartID = cartID, // Trả về CartID đã lưu
                    Name = product.Name,
                    Price = product.Detail.Price,
                    isMultiple = existingCartItem != null ? existingCartItem.isMultiple : request.isMultiple,
                    ProductID = product.ProductID,
                    Quantity = existingCartItem != null ? existingCartItem.Quantity : request.Quantity, // Số lượng cập nhật
                    Total = product.Detail.Price * (existingCartItem != null ? existingCartItem.Quantity : request.Quantity),
                    UserID = request.UserID,
                    PreImg = product.PreImg
                };

                return cartRes; // Trả về kết quả nếu lưu thành công
            }

            // Nếu lưu không thành công (saveResult <= 0), có thể ném ngoại lệ hoặc trả về null
            throw new Exception("Failed to add product to cart.");
        }

        public async Task<bool> UpdateCartItem(Guid cartId, int quantity)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null || cartItem.DeleteAt != null)
            {
                return false; // Không tìm thấy sản phẩm trong giỏ
            }

            cartItem.Quantity = quantity;
            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<bool> ClearCart(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid UserID. Please provide a valid GUID.");
            }

            var cartItems = await _context.Carts
                .Where(x => x.UserID == userId && x.DeleteAt == null)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return false; // Không có mục nào để xóa
            }

            foreach (var item in cartItems)
            {
                item.DeleteAt = DateTime.UtcNow; // Đánh dấu là đã xóa
            }

            _context.Carts.UpdateRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteCartItem(DeleteCartItem request)
        {

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(x => x.CartID == request.CartId && x.UserID == request.UserID && x.DeleteAt == null);

            if (cartItem == null)
            {
                return false;
            }

            cartItem.DeleteAt = DateTime.UtcNow;

            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
