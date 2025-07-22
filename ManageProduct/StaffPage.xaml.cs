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
    /// Interaction logic for StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private readonly StaffService _staffService;

        public StaffPage()
        {
            InitializeComponent();
            var context = new ManageProductContext();
            var repo = new StaffRepository(context);
            _staffService = new StaffService(repo);
            LoadStaffs();
            if (CurrentSession.CurrentUser.RoleID != 1)
            {
                AddStaffBtn.Visibility = Visibility.Collapsed;
                StaffDataGrid.Columns[3].Visibility = Visibility.Collapsed;
            }
        }

        private void LoadStaffs()
        {
            var staffs = _staffService.GetStaffs();
            StaffDataGrid.ItemsSource = staffs;
            if (this.FindName("StaffCountText") is TextBlock countText)
            {
                countText.Text = $"Total: {staffs.Count} staffs";
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var createStaffWindow = new CreateStaffWindow();
            if (createStaffWindow.ShowDialog() == true)
            {
                string name = createStaffWindow.StaffName?.Trim();
                string email = createStaffWindow.StaffEmail?.Trim();

                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Full Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra định dạng email
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    _staffService.AddStaff(name, email);
                    MessageBox.Show("Staff added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadStaffs();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void EditStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag == null) return;

            int staffId = Convert.ToInt32(button.Tag);
            var staff = _staffService.GetStaffs().FirstOrDefault(c => c.UserID == staffId);
            if (staff == null) return;

            var editDialog = new EditStaffWindow(staff.FullName, staff.Email);
            if (editDialog.ShowDialog() == true)
            {
                string newName = editDialog.StaffName.Trim();
                string newEmail = editDialog.StaffEmail.Trim();

                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newEmail))
                {
                    MessageBox.Show("Full Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    _staffService.UpdateStaff(staffId, newName, newEmail);
                    MessageBox.Show("Staff updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadStaffs();
                }
                catch (InvalidOperationException ex)
                {
                    // Lỗi trùng email hoặc lỗi khác do bạn định nghĩa ở tầng Service
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void DeleteStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int staffId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to delete this staff?",
                    "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _staffService.DeleteStaff(staffId);
                    LoadStaffs();
                }
            }
        }

        public void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int staffId = Convert.ToInt32(button.Tag);
                var result = MessageBox.Show("Are you sure you want to reset password this staff?",
                    "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _staffService.resetPassword(staffId);
                    MessageBox.Show("Reset successfully");
                }
            }
        }
    }
}
