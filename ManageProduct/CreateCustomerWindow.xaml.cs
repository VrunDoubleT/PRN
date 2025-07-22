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
    /// Interaction logic for CreateCustomerWindow.xaml
    /// </summary>
    public partial class CreateCustomerWindow : Window
    {
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }

        public CreateCustomerWindow()
        {
            InitializeComponent();
        }

        public CreateCustomerWindow(string name, string email) : this()
        {
            txtFullName.Text = name;
            txtEmail.Text = email;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CustomerName = txtFullName.Text.Trim();
            CustomerEmail = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(CustomerEmail))
            {
                MessageBox.Show("Full Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
