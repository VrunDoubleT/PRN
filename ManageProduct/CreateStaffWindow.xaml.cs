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
    /// Interaction logic for CreateStaffWindow.xaml
    /// </summary>
    public partial class CreateStaffWindow : Window
    {
        public string StaffName { get; private set; }
        public string StaffEmail { get; private set; }

        public CreateStaffWindow()
        {
            InitializeComponent();
        }

        public CreateStaffWindow(string name, string email) : this()
        {
            txtFullName.Text = name;
            txtEmail.Text = email;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StaffName = txtFullName.Text.Trim();
            StaffEmail = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(StaffName) || string.IsNullOrWhiteSpace(StaffEmail))
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
