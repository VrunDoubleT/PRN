using ManageProduct.BLL.Services;
using ManageProduct.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Window> trollWindows = new();
        private ProductService _productService = new();

        public MainWindow()
        {
            InitializeComponent();
            ContentArea.Content = new DashboardPage();
            LoadInfo();
        }

        private void LoadInfo()
        {
            if (CurrentSession.CurrentUser != null)
            {
                NameTxt.Text = string.IsNullOrEmpty(CurrentSession.CurrentUser.FullName)
                    ? "Can not find name"
                    : CurrentSession.CurrentUser.FullName;

                RoleTxt.Text = CurrentSession.CurrentUser.RoleID == 1 ? "Admin" : "Staff";
            }
            else
            {
                NameTxt.Text = "User not logged in";
                RoleTxt.Text = "Role not available";
            }
        }


        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DashboardPage();
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ProductPage();
        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CategoryPage();
        }

        private void BrandBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new BrandPage();
        }

        private void StaffBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new StaffPage();
        }

        private void CustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CustomerPage();
        }

        public void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            CurrentSession.CurrentUser = null;
            this.Close();
        }
    }
}