using ManageProduct.BLL.Services;
using ManageProduct.DAL;
using ManageProduct.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ManageProduct.DAL.Repositories.UserRepositories;

namespace ManageProduct
{
    /// <summary>
    /// Interaction logic for BrandPage.xaml
    /// </summary>
    public partial class BrandPage : Page
    {
        private readonly BrandService _brandService;

        public BrandPage()
        {
            InitializeComponent();
            var context = new ManageProductContext();
            var repo = new BrandRepository(context);
            _brandService = new BrandService(repo);
            LoadBrands();
            if (CurrentSession.CurrentUser.RoleID != 1)
            {
                AddBrandBtn.Visibility = Visibility.Collapsed;
                BrandDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            }
        }

        private void LoadBrands()
        {
            var brands = _brandService.GetBrands();
            BrandDataGrid.ItemsSource = brands;

            if (this.FindName("BrandCountText") is TextBlock brandCountText)
            {
                brandCountText.Text = $"Tổng: {brands.Count} thương hiệu";
            }
        }

        private void AddBrandBtn_Click(object sender, RoutedEventArgs e)
        {
            //if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            //{
            //    MessageBox.Show("You do not have permission to add brands!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            var createBrandWindow = new CreateBrand();
            if (createBrandWindow.ShowDialog() == true)
            {
                string name = createBrandWindow.BrandName;

                bool success = _brandService.AddBrand(name);
                if (!success)
                {
                    MessageBox.Show("Brand name already exists! Please choose a different name.",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                LoadBrands();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            //if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            //{
            //    MessageBox.Show("You do not have permission to edit the brand!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            var button = sender as Button;
            if (button?.Tag != null)
            {
                int brandId = Convert.ToInt32(button.Tag);
                var brand = _brandService.GetBrands().FirstOrDefault(b => b.BrandID == brandId);
                if (brand == null) return;

                var editDialog = new CreateBrand(brand.Name);
                if (editDialog.ShowDialog() == true)
                {
                    bool success = _brandService.UpdateBrand(brandId, editDialog.BrandName);
                    if (!success)
                    {
                        MessageBox.Show("Brand name already exists! Please choose a different name.",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    LoadBrands();
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // if (CurrentSession.CurrentUser != null && CurrentSession.CurrentUser.RoleID == 2)
            // {
            //     MessageBox.Show("You do not have permission to delete the brand!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
            //     return;
            // }

            var button = sender as Button;
            if (button?.Tag != null)
            {
                int brandId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to delete this brand?",
                    "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = _brandService.DeleteBrand(brandId);
                    if (!success)
                    {
                        MessageBox.Show("Cannot delete this brand because there are still products linked to it!",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Brand deleted successfully!",
                            "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadBrands();
                    }
                }
            }
        }
    }
}
