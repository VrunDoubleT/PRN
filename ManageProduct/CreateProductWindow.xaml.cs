using ManageProduct.BLL.Services;
using ManageProduct.DAL.Entities;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ManageProduct
{
    /// <summary>
    /// Interaction logic for CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        private ProductPage _productPage;
        ProductService _productService = new();
        public Product NewProduct { get; set; }

        public CreateProductWindow(ProductPage productPage)
        {
            InitializeComponent();
            _productPage = productPage;
            LoadData();
        }

        private void LoadData()
        {
            // Load categories - thay thế bằng code thực tế để load từ database
            LoadCategories();

            // Load brands - thay thế bằng code thực tế để load từ database
            LoadBrands();
        }

        private void LoadCategories()
        {
            // Ví dụ dữ liệu mẫu - thay thế bằng code thực tế
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Điện thoại" },
                new Category { CategoryID = 2, CategoryName = "Laptop" },
                new Category { CategoryID = 3, CategoryName = "Tai nghe" },
                new Category { CategoryID = 4, CategoryName = "Phụ kiện" }
            };

            cmbCategory.ItemsSource = categories;
        }

        private void LoadBrands()
        {
            // Ví dụ dữ liệu mẫu - thay thế bằng code thực tế
            var brands = new List<Brand>
            {
                new Brand { BrandID = 1, BrandName = "Samsung" },
                new Brand { BrandID = 2, BrandName = "Apple" },
                new Brand { BrandID = 3, BrandName = "Xiaomi" },
                new Brand { BrandID = 4, BrandName = "Huawei" }
            };

            cmbBrand.ItemsSource = brands;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                NewProduct = new Product
                {
                    Title = txtTitle.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                    Price = decimal.Parse(txtPrice.Text.Trim()),
                    StockQuantity = int.Parse(txtStockQuantity.Text.Trim()),
                    CategoryID = (int)cmbCategory.SelectedValue,
                    BrandID = (int)cmbBrand.SelectedValue,
                    ImageURL = string.IsNullOrWhiteSpace(txtImageURL.Text) ? null : txtImageURL.Text.Trim()
                };
                _productService.CreateProduct(NewProduct);
                _productPage.LoadProductData();
                this.DialogResult = true;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                txtImageURL.Text = openFileDialog.FileName;
            }
        }

        private bool ValidateInput()
        {
            var errorMessages = new List<string>();

            // Kiểm tra tên sản phẩm
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorMessages.Add("Title is not empty");
            }

            // Kiểm tra giá
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                errorMessages.Add("Price is not empy");
            }
            else if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                errorMessages.Add("Price must be positive number");
            }

            // Kiểm tra số lượng tồn kho
            if (string.IsNullOrWhiteSpace(txtStockQuantity.Text))
            {
                errorMessages.Add("Quantity is not empty");
            }
            else if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stock) || stock < 0)
            {
                errorMessages.Add("Quantity must be positive number");
            }

            // Kiểm tra danh mục
            if (cmbCategory.SelectedValue == null)
            {
                errorMessages.Add("Please select category");
            }

            // Kiểm tra thương hiệu
            if (cmbBrand.SelectedValue == null)
            {
                errorMessages.Add("Please select brand");
            }

            // Hiển thị lỗi nếu có
            if (errorMessages.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Error confirm",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
    }
}

