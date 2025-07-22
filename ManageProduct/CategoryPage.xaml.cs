using System.Windows;
using ManageProduct.DAL.Repositories;
using ManageProduct.DAL.Entities;
using ManageProduct.BLL.Services;
using ManageProduct.DAL;
using System.Windows.Controls;
using System.Linq;
using static ManageProduct.DAL.Repositories.UserRepositories;

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

            if (this.FindName("CategoryCountText") is TextBlock categoryCountText)
            {
                categoryCountText.Text = $"Tổng: {categories.Count} danh mục";
            }
        }

        private void AddCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            {
                MessageBox.Show("You do not have permission to add categories!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var createCategoryWindow = new CreateCategory();
            if (createCategoryWindow.ShowDialog() == true)
            {
                string name = createCategoryWindow.CategoryName;
                string desc = createCategoryWindow.CategoryDescription;


                bool added = _categoryService.AddCategory(name, desc);
                if (!added)
                {
                    MessageBox.Show("The category name already exists! Please choose a different name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LoadCategories();
            }
        }



        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            {
                MessageBox.Show("You do not have permission to edit the category!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
            if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            {
                MessageBox.Show("You do not have permission to delete the category!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var button = sender as Button;
            if (button?.Tag != null)
            {
                int categoryId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to delete this category?",
                    "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = _categoryService.DeleteCategory(categoryId);
                    if (!success)
                    {
                        MessageBox.Show("Cannot delete this category because there are still products linked to it!",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Category deleted successfully!",
                            "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCategories();
                    }
                }
            }
        }



    }
}
