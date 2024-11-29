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
            return await _context.Set<CategoriesBrand>()
                .Where(cb => cb.CategoryID == categoryId)
                .Join(
                    _context.Set<Brand>(),
                    cb => cb.BrandID,
                    b => b.BrandID,
                    (cb, b) => b
                )
                .ToListAsync();
        }


        public async Task<List<Category>> GetCategoriesByBrandId(Guid brandId)
        {
            return await _context.Set<CategoriesBrand>()
                .Where(cb => cb.BrandID == brandId)
                .Join(
                    _context.Set<Category>(),
                    cb => cb.CategoryID,
                    c => c.CategoryID,
                    (cb, c) => c
                )
                .ToListAsync();
        }


        public async Task<bool> AddCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var exists = await _context.Set<CategoriesBrand>()
                .AnyAsync(cb => cb.CategoryID == categoryId && cb.BrandID == brandId);

            if (exists) return false;

            var newLink = new CategoriesBrand
            {
                CategoryBrandID = Guid.NewGuid(),
                CategoryID = categoryId,
                BrandID = brandId
            };

            await _context.Set<CategoriesBrand>().AddAsync(newLink);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RemoveCategoryBrandLink(Guid categoryId, Guid brandId)
        {
            var link = await _context.Set<CategoriesBrand>()
                .FirstOrDefaultAsync(cb => cb.CategoryID == categoryId && cb.BrandID == brandId);

            if (link == null) return false;

            _context.Set<CategoriesBrand>().Remove(link);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
