using BEforREACT.Data;
using BEforREACT.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace BEforREACT.Services
{
    public class ProductDetailServices
    {
        private readonly DataContext _context;
        public ProductDetailServices(DataContext context)
        {
            _context = context;

        }

        public async Task<ProductDetail> AddProductDetail(ProductDetail newDetail)
        {
            //if (newDetail == null)
            //    throw new ArgumentNullException(nameof(newDetail));

            _context.ProductDetails.Add(newDetail);
            await _context.SaveChangesAsync();
            return newDetail;
        }


        public async Task<ProductDetail> UpdateProductDetail(Guid detailId, ProductDetail updatedDetail)
        {
            // Tìm bản ghi ProductDetail
            var existingDetail = await _context.ProductDetails.FindAsync(detailId);
            if (existingDetail == null)
                throw new KeyNotFoundException("ProductDetail not found!");

            // Kiểm tra xem CategoryID có tồn tại không
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == updatedDetail.CategoryID);
            if (!categoryExists)
                throw new KeyNotFoundException("Category not found!");

            // Kiểm tra xem BrandID có tồn tại không
            var brandExists = await _context.Brands.AnyAsync(b => b.BrandID == updatedDetail.BrandID);
            if (!brandExists)
                throw new KeyNotFoundException("Brand not found!");

            // Cập nhật các trường cần thiết
            existingDetail.CategoryID = updatedDetail.CategoryID; // Cập nhật danh mục
            existingDetail.BrandID = updatedDetail.BrandID;       // Cập nhật thương hiệu
            existingDetail.Price = updatedDetail.Price;           // Cập nhật giá
            existingDetail.detailDes = updatedDetail.detailDes;   // Cập nhật mô tả chi tiết
            existingDetail.Rating = updatedDetail.Rating;         // Cập nhật đánh giá
            existingDetail.Stock = updatedDetail.Stock;           // Cập nhật tồn kho

            // Lưu thay đổi vào database
            await _context.SaveChangesAsync();
            return existingDetail;
        }
    }
}
