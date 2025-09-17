using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Model;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _context;
        public CategoryService(BlogDbContext context)
        {
            _context= context;
        }


        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            //var Categories=await _context.Categories.ToListAsync();
            var Categories = _context.Categories.Select(c => new Category
            {
                CategoryName = c.CategoryName,
                Id = c.Id,
                // Posts=c.Posts
            });

            return Categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);//FindAsync used for id only
            //return await _context.Categories.FirstAsync(c=>c.Id==id);//take first match if not exist it throw error and lag project
            //return await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);//take first match if not exist will be null
            //return await _context.Categories.SingleAsync(c => c.Id == id);//if value we want repeated will throw error
            //return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);//if value we want repeated will throw error


        }
        public async Task<Category> CreateAsync(CategoryDTo category)
        {
            var Category = new Category
            {
                CategoryName = category.CategoryName,
            };
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Category;
        }


        public async Task<bool> UpdateAsync(int id, CategoryDTo category)
        {
            var oldCategory = await GetByIdAsync(id);
            if (oldCategory == null) return false;
            oldCategory.CategoryName = category.CategoryName;
            _context.Categories.Update(oldCategory);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(CategoryDTo category)
        {
            var oldCategory = await GetByIdAsync(category.Id);
            if (oldCategory == null) return false;
            oldCategory.CategoryName = category.CategoryName;
            _context.Categories.Update(oldCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var Category =await GetByIdAsync(id);
            if (Category == null) return false;
             _context.Remove(Category);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
