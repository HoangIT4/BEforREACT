using BEforREACT.Data;
using BEforREACT.Data.Entities;

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

        //public async Task<Brand> CreateBrand(Brand brand, List<Guid>? categoryIds = null)
        //{
        //    brand.BrandID = Guid.NewGuid();

        //    if (categoryIds != null && categoryIds.Any())
        //    {
        //        brand.CategoriesBrands = categoryIds.Select(id => new CategoriesBrand
        //        {
        //            CategoryID = id,
        //            BrandID = brand.BrandID
        //        }).ToList();
        //    }

        //    _context.Brands.Add(brand);
        //    await _context.SaveChangesAsync();
        //    return brand;
        //}




        public async Task<Brand> UpdateBrand(Guid id, Brand brand)
        {
            if (id != brand.BrandID)
            {
                throw new ArgumentException("Brand ID ko tồn tại");
            }

            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return brand;
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
