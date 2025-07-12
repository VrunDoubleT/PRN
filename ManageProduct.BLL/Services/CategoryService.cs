using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageProduct.DAL.Entities;
using ManageProduct.DAL.Repositories;

namespace ManageProduct.BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

       
        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public void AddCategory(string categoryName, string description)
        {
            _categoryRepository.AddCategory(categoryName, description);
        }

        public void UpdateCategory(int categoryId, string categoryName, string description)
        {
            _categoryRepository.UpdateCategory(categoryId, categoryName, description);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);
        }
    }
}
