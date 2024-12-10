using BEforREACT.Data;
using BEforREACT.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace BEforREACT.Services
{
    public class CategoriesBrandServices
    {
        private readonly DataContext _context;

        public CategoriesBrandServices(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetBrandsByCategoryId(Guid categoryId)
        {
            return await _context.CategoriesBrands
                .Where(cb => cb.CategoryID == categoryId)
                .Include(cb => cb.Brand)
                .Select(cb => cb.Brand)
                .ToListAsync();
        }


        public async Task<List<Category>> GetCategoriesByBrandId(Guid brandId)
        {
            return await _context.CategoriesBrands
                .Where(cb => cb.BrandID == brandId)
                .Include(cb => cb.Category) // Eager load Category
                .Select(cb => cb.Category)
                .ToListAsync();
        }


        public async Task<bool> AddCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var exists = await _context.CategoriesBrands
                .AnyAsync(cb => cb.CategoryID == categoryId && cb.BrandID == brandId);

            if (exists) return false; // Nếu đã tồn tại liên kết, trả về false

            var newLink = new CategoriesBrand
            {
                CategoryBrandID = Guid.NewGuid(),
                CategoryID = categoryId,
                BrandID = brandId
            };

            await _context.CategoriesBrands.AddAsync(newLink);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var link = await _context.CategoriesBrands
                .FirstOrDefaultAsync(cb => cb.CategoryID == categoryId && cb.BrandID == brandId);

            if (link == null) return false; // Nếu không tìm thấy liên kết, trả về false

            _context.CategoriesBrands.Remove(link);
            await _context.SaveChangesAsync();
            return true; // Trả về true nếu xóa liên kết thành công
        }
    }
}
