using System.Windows;
using ManageProduct.DAL.Repositories;
using ManageProduct.DAL.Entities;
using ManageProduct.BLL.Services;
using ManageProduct.DAL;
using System.Windows.Controls;
using System.Linq;

namespace ManageProduct
{
    public partial class CategoryPage : Page
    {
        private readonly CategoryService _categoryService;

        public CategoryPage()
        {
            InitializeComponent();
            var context = new ManageProductContext();
            var repo = new CategoryRepository(context);
            _categoryService = new CategoryService(repo);
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetCategories();
            CategoryDataGrid.ItemsSource = categories;
            // Fix: Check if CategoryCountText exists before using
            if (this.FindName("CategoryCountText") is TextBlock categoryCountText)
            {
                categoryCountText.Text = $"Tổng: {categories.Count} danh mục";
            }
        }

        private void AddCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var createCategoryWindow = new CreateCategory();
            if (createCategoryWindow.ShowDialog() == true)
            {
                string name = createCategoryWindow.CategoryName;
                string desc = createCategoryWindow.CategoryDescription;
                // Fix: Use only name, since AddCategory only accepts one argument
                _categoryService.AddCategory(name, desc);
                LoadCategories();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int categoryId = Convert.ToInt32(button.Tag);
                var category = _categoryService.GetCategories().FirstOrDefault(c => c.CategoryID == categoryId);
                if (category == null) return;

                var editDialog = new CreateCategory(category.Name, category.Description ?? "");
                if (editDialog.ShowDialog() == true)
                {
                    _categoryService.UpdateCategory(categoryId, editDialog.CategoryName, editDialog.CategoryDescription);
                    LoadCategories();
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int categoryId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to delete this category?",
                    "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _categoryService.DeleteCategory(categoryId);
                    LoadCategories();
                }
            }
        }

    }
}
