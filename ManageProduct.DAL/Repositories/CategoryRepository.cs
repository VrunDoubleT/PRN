using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
namespace ManageProduct.DAL.Repositories
{ 
    public class CategoryRepository
    {
        private readonly ManageProductContext _context;

        public CategoryRepository(ManageProductContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public void AddCategory(string categoryName, string description)
        {
            var category = new Category { Name = categoryName, Description = description };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(int categoryId, string categoryName, string description)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                category.Name = categoryName;
                category.Description = description;
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

    }
}
