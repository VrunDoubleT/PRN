using ManageProduct.BLL.Services;
using ManageProduct.DAL.Entities;
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

namespace ManageProduct
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        ProductService _productService = new();
        public void LoadProductData()
        {
            ProductDataGrid.ItemsSource = null;
            List<Product> products = _productService.GetProducts();
            ProductDataGrid.ItemsSource = products;

            TotalProductsText.Text = products.Count.ToString();
            LowStockText.Text = products.Count(p => p.StockQuantity < 5).ToString();
            TotalValueText.Text = "$" + products.Sum(p => p.Price * p.StockQuantity).ToString("N2");
            AvgPriceText.Text = "$" + products.Average(p => p.Price).ToString("N2");
        }

        public ProductPage()
        {
            InitializeComponent();
            LoadProductData();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadProductData();
        }

        private void DeleteBtn_Click(Object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int productId = (int)btn.Tag;

            // Xác nhận trước khi xóa
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this product (ID: " + productId + ")?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _productService.DeleteProduct(productId);
                LoadProductData();
                MessageBox.Show("Product deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddBtn_Click(Object sender, RoutedEventArgs e)
        {
            CreateProductWindow createProductWindow = new CreateProductWindow(this);
            createProductWindow.ShowDialog();
        }
    }
}
