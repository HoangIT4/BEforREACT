using BEforREACT.Data;

namespace BEforREACT.Services
{
    public class CartServices
    {
        private readonly DataContext _context;
        public CartServices(DataContext context)
        {
            _context = context;

        }

        //public async Task AddToCart(Guid userId, Guid productId, int quantity)
        //{
        //    var product = await _context.Products
        //          .Include(p => p.Detail) // Bao gồm ProductDetails
        //          .FirstOrDefaultAsync(p => p.ProductID == productId);

        //    if (product == null || product.Detail == null)
        //    {
        //        throw new Exception("Product or product detail not found.");
        //    }

        //    // Lấy ProductDetails từ Product
        //    var productDetail = product.Detail;

        //    if (quantity > productDetail.Stock)
        //    {
        //        throw new Exception("Not enough stock available.");
        //    }

        //    var cartItem = await _context.Carts
        //        .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

        //    if (cartItem != null)
        //    {

        //        if (cartItem.Quantity + quantity > productDetail.Stock)
        //        {
        //            throw new Exception("Not enough stock available.");
        //        }

        //        cartItem.Quantity += quantity;
        //    }
        //    else
        //    {
        //        var newCartItem = new Cart
        //        {
        //            CartID = Guid.NewGuid(),
        //            UserID = userId,
        //            ProductID = productId,
        //            Quantity = quantity
        //        };

        //        await _context.Carts.AddAsync(newCartItem);
        //    }
        //    await _context.SaveChangesAsync();
        //}

    }
}
