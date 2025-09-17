using Blog.Core.DTos;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface ICategoryService
    {
        //get all
        Task<IEnumerable<Category>> GetAllAsync();
        //get by id
        Task<Category>GetByIdAsync(int id);
        //create
        //Task<Category> CreateAsync(Category category);//bad need to create list of post so we use DTos(Data transfare objects) to use same class with needs and remove any things unwanted
        Task<Category> CreateAsync(CategoryDTo category);
        //update
        Task<bool>UpdateAsync(CategoryDTo category);
        Task<bool>UpdateAsync(int id, CategoryDTo category);
        //delete
        Task<bool> DeleteAsync(int id);
    }
}
