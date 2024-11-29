using BEforREACT.Data;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class CategoryServices
    {
        private readonly DataContext _context;
        public CategoryServices(DataContext context)
        {
            _context = context;

        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }


        public bool AddCategory(CategoryDTO request)
        {
            var category = new Category()
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = request.CategoryName,

            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }


        public async Task<Category> UpdateCategory(Guid id, Category category)
        {
            if (id != category.CategoryID)
            {
                throw new ArgumentException("Category ID not found");
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
