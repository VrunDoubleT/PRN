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
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        DashboardService _dashboardService = new();
        public DashboardPage()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Load statistics
            TotalProductsText.Text = _dashboardService.getTotalProducts().ToString();
            TotalCategoriesText.Text = _dashboardService.getTotalCategories().ToString();
            TotalBrandsText.Text = _dashboardService.getTotalBrands().ToString();
            TotalUsersText.Text = _dashboardService.getTotalUsers().ToString();
            TotalStockText.Text = _dashboardService.getTotalStocks().ToString();

            // Load top categories
            TopCategoriesGrid.ItemsSource = _dashboardService.getTopCategories();

            // Load top brands
            TopBrandsGrid.ItemsSource = _dashboardService.getTopBrands();

            //
            RolesDistributionGrid.ItemsSource = _dashboardService.GetTopRoles();

            // Load price statistics
            AvgPriceText.Text = _dashboardService.getAveragePrice().ToString("C");
            MinPriceText.Text = _dashboardService.getMinPrice().ToString("C");
            MaxPriceText.Text = _dashboardService.getMaxPrice().ToString("C");
            TotalValueText.Text = _dashboardService.getTotalInventoryValue().ToString("C");

            LastUpdatedText.Text = $"Last updated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
        }
    }
}
