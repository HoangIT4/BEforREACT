using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class BrandServices
    {
        private readonly DataContext _context;

        public BrandServices(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandById(Guid id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public bool AddBrand(BrandDTO request)
        {
            var brand = new Brand()
            {
                BrandID = Guid.NewGuid(),
                BrandName = request.BrandName,
            };


            _context.Brands.Add(brand);
            _context.SaveChanges();
            return true;
        }




        public async Task<BrandDTO> UpdateBrand(Guid id, BrandDTO brandDTO)
        {
            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand == null)
            {
                throw new KeyNotFoundException("Brand not found.");
            }

            // Cập nhật các thuộc tính của thương hiệu từ BrandDTO
            existingBrand.BrandName = brandDTO.BrandName;
            existingBrand.CreatedAt = brandDTO.CreatedAt;  // Cập nhật thêm nếu có

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Brands.Update(existingBrand);
            await _context.SaveChangesAsync();

            // Trả về BrandDTO đã cập nhật
            return new BrandDTO
            {
                BrandID = existingBrand.BrandID,
                BrandName = existingBrand.BrandName,
                CreatedAt = existingBrand.CreatedAt
            };
        }


        public async Task<bool> DeleteBrand(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return false;
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
