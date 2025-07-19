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

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var createStaffWindow = new CreateStaffWindow();
            if (createStaffWindow.ShowDialog() == true)
            {
                string name = createStaffWindow.StaffName;
                string email = createStaffWindow.StaffEmail;
                _staffService.AddStaff(name, email);
                LoadStaffs();
            }
        }

        private void EditStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int staffId = Convert.ToInt32(button.Tag);
                var staff = _staffService.GetStaffs().FirstOrDefault(c => c.UserID == staffId);
                if (staff == null) return;

                var editDialog = new EditStaffWindow(staff.FullName, staff.Email);
                if (editDialog.ShowDialog() == true)
                {
                    _staffService.UpdateStaff(staffId, editDialog.StaffName, editDialog.StaffEmail);
                    LoadStaffs();
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
    }
}
