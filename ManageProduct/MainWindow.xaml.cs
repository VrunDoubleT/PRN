using ManageProduct.BLL.Services;
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

        private void StopTrollBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var win in trollWindows)
            {
                win.Close();
            }

            trollWindows.Clear();
            StopTrollBtn.Visibility = Visibility.Collapsed;
        }


        private async void AOMA_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            trollWindows.Clear();

            string[] trollMessages = new string[]
{
    "Critical system error detected. Immediate action required.",
    "System security compromised. All sessions are exposed.",
    "High-risk malware infection in progress.",
    "Unauthorized access detected. Data breach suspected.",
    "All system files are being reconfigured.",
    "Your credentials may have been stolen.",
    "Firewall has been disabled remotely.",
    "Network traffic is being rerouted to unknown IP.",
    "System performance is being throttled by an unknown process.",
    "Sensitive files are being accessed without permission.",
    "Remote control of your machine has been activated.",
    "Encryption of critical files is underway.",
    "Background processes show unusual behavior.",
    "Security logs indicate multiple failed login attempts.",
    "All user activity is being recorded and sent externally.",
    "System registry is being modified.",
    "Antivirus software is no longer active.",
    "BIOS update initiated without user approval.",
    "Device drivers are being uninstalled silently.",
    "A system shutdown has been scheduled by an unknown source."
};

            for (int i = 0; i < 38; i++)
            {
                Window win = new Window
                {
                    Title = $"⚠️ ERROR #{i + 1}",
                    Width = 460,
                    Height = 200,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStyle = WindowStyle.None,
                    Background = Brushes.Black,
                    BorderBrush = Brushes.Red,
                    BorderThickness = new Thickness(4),
                    Topmost = true,
                    Content = new Border
                    {
                        Background = Brushes.Black,
                        BorderBrush = Brushes.Red,
                        BorderThickness = new Thickness(2),
                        Padding = new Thickness(20),
                        Child = new TextBlock
                        {
                            Text = trollMessages[random.Next(trollMessages.Length)],
                            FontSize = 20,
                            FontWeight = FontWeights.ExtraBold,
                            Foreground = Brushes.Red,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center,
                            TextWrapping = TextWrapping.Wrap
                        }
                    }
                };


                win.Left = random.Next(0, (int)(SystemParameters.PrimaryScreenWidth - win.Width));
                win.Top = random.Next(0, (int)(SystemParameters.PrimaryScreenHeight - win.Height));

                trollWindows.Add(win);
                win.Show();
            }
            await Task.Delay(TimeSpan.FromMinutes(1));
            foreach (var win in trollWindows)
            {
                win.Topmost = false;
            }

            StopTrollBtn.Visibility = Visibility.Visible;
            StopTrollBtn.Visibility = Visibility.Visible;
        }
    }
}