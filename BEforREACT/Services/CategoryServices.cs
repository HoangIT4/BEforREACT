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


        public async Task<bool> AddCategory(CategoryDTO request)
        {
            var category = new Category()
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = request.CategoryName,

            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<CategoryDTO> UpdateCategory(Guid id, CategoryDTO categoryDTO)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            existingCategory.CategoryName = categoryDTO.CategoryName;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryID = existingCategory.CategoryID,
                CategoryName = existingCategory.CategoryName,
                //CreatedAt = existingCategory.CreatedAt
            };
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
