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
using ManageProduct.BLL.Services;
using ManageProduct.DAL.Repositories;
using ManageProduct.DAL;
using static ManageProduct.DAL.Repositories.UserRepositories;

namespace ManageProduct
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private readonly CustomerService _customerService;

        public CustomerPage()
        {
            InitializeComponent();
            var context = new ManageProductContext();
            var repo = new CustomerRepository(context);
            _customerService = new CustomerService(repo);
            LoadCustomers();
            if (CurrentSession.CurrentUser.RoleID != 1)
            {
                //AddCustomerBtn.Visibility = Visibility.Collapsed;
                CustomerDataGrid.Columns[3].Visibility = Visibility.Collapsed;
            }
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetCustomers();
            CustomerDataGrid.ItemsSource = customers;
            if (this.FindName("CustomerCountText") is TextBlock countText)
            {
                countText.Text = $"Total: {customers.Count} customers";
            }
        }

        private void AddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var createCustomerWindow = new CreateCustomerWindow();
            if (createCustomerWindow.ShowDialog() == true)
            {
                string name = createCustomerWindow.CustomerName;
                string email = createCustomerWindow.CustomerEmail;
                _customerService.AddCustomer(name, email);
                LoadCustomers();
            }
        }

        private void EditCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int customerId = Convert.ToInt32(button.Tag);
                var customer = _customerService.GetCustomers().FirstOrDefault(c => c.UserID == customerId);
                if (customer == null) return;

                var editDialog = new EditCustomerWindow(customer.FullName, customer.Email);
                if (editDialog.ShowDialog() == true)
                {
                    _customerService.UpdateCustomer(customerId, editDialog.CustomerName, editDialog.CustomerEmail);
                    LoadCustomers();
                }
            }
        }

        private void DeleteCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int customerId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to delete this customer?",
                    "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _customerService.DeleteCustomer(customerId);
                    LoadCustomers();
                }
            }
        }
    }
}
