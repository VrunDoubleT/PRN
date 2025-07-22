using ManageProduct.DAL;
using ManageProduct.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private Product _product;
        private readonly ManageProductContext _context = new ManageProductContext()!;
        
        public EditProductWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductData();
        }

        private void LoadProductData()
        {
            var _categories = _context.Categories.ToList();
            var _brands = _context.Brands.ToList();
            txtTitle.Text = _product.Title;
            txtDescription.Text = _product.Description;
            txtPrice.Text = _product.Price.ToString();
            txtStockQuantity.Text = _product.StockQuantity.ToString();
            cmbCategory.ItemsSource = _categories;
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedValuePath = "CategoryID";
            cmbCategory.SelectedValue = _product.CategoryID;

            cmbBrand.ItemsSource = _brands;
            cmbBrand.DisplayMemberPath = "Name";
            cmbBrand.SelectedValuePath = "BrandID";
            cmbBrand.SelectedValue = _product.BrandID;
        }

        public void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var productToUpdate = _context.Products.FirstOrDefault(p => p.ProductID == _product.ProductID);
            if (productToUpdate == null)
            {
                MessageBox.Show("Product not found");
                return;
            }
            productToUpdate.Title = txtTitle.Text;
            productToUpdate.Description = txtDescription.Text;
            productToUpdate.Price = decimal.TryParse(txtPrice.Text, out var price) ? price : 0;
            productToUpdate.StockQuantity = int.TryParse(txtStockQuantity.Text, out var quantity) ? quantity : 0;
            productToUpdate.CategoryID = (int)cmbCategory.SelectedValue;
            productToUpdate.BrandID = (int)cmbBrand.SelectedValue;
            _context.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }


        public void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
