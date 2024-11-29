using BEforREACT.Data;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class ProductServices
    {
        private readonly DataContext _context;
        public ProductServices(DataContext context)
        {
            _context = context;

        }
        public async Task<List<ProductItemViewDTO>> GetProductItemsAsync()
        {
            var products = await _context.Products
            .Include(p => p.Detail) // Bao gồm thông tin chi tiết từ bảng ProductDetail
            .ToListAsync();

            var result = products.Select(p => new ProductItemViewDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Src = p.Src,
                PreImg = p.PreImg,
                Price = p.Detail?.Price ?? 0
            }).ToList();

            return result;
        }


        //public async Task<ProductsDetailDTO> GetProductDetailAsync(Guid productId)
        //{

        //    var products = await _context.Products
        //   .Include(p => p.Detail) // Bao gồm thông tin chi tiết từ bảng ProductDetail
        //   .ToListAsync();

        //    var result = await _context.Products
        //        .Where(p => p.ProductID == productId)
        //        .Select(p => new ProductsDetailDTO
        //        {
        //            ProductID = p.ProductID,
        //            Name = p.Name,
        //            Src = p.Src,
        //            PreImg = p.PreImg,
        //            detailDes = p.detailDes, // Giả định bạn có trường này
        //            Description = p.Description,
        //            Price = p.Price,
        //            Stock = p.Stock,
        //            Rating = p.Rating,
        //            Brands = new BrandDTO
        //            {
        //                // Giả định bạn có bảng BrandDTO
        //                BrandID = p.Brand.BrandID,
        //                BrandName = p.Brand.Name
        //            },
        //            Categories = new CategoryDTO
        //            {
        //                // Giả định bạn có bảng CategoryDTO
        //                CategoryID = p.Category.CategoryID,
        //                CategoryName = p.Category.Name
        //            },
        //            FormattedPrice = p.Price.ToString("#,0.###") + " đ"
        //        })
        //        .FirstOrDefaultAsync();

        //    return product;


        //}
    }
}
