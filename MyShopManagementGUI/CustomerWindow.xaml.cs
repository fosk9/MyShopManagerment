using MyShopManagementBO;
using MyShopManagementService;
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

namespace MyShopManagementGUI
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private User currentUser;
        private readonly IProductService productService = new ProductService();
        private List<Product> currentProductList;
        private const string NOIMG = "https://t3.ftcdn.net/jpg/04/34/72/82/360_F_434728286_OWQQvAFoXZLdGHlObozsolNeuSxhpr84.jpg";
        public CustomerWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            userProfileStackPanel.DataContext = user;
            txtWelcome.Text = "Welcome " + user.Name + "!";
            LoadData();
            var cmbFilterSources = productService.GetCategories();
            cmbFilterSources.Insert(0, new Category() { Id = 0, Name = "All" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.ItemsSource = cmbFilterSources;
            //MessageBox.Show("Welcome to shop!", "Login Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadData()
        {
            currentProductList = productService.GetAll().Where(item => item.Status).ToList();
            dgProducts.ItemsSource = currentProductList;
            dgProducts.SelectedIndex = 0;
        }

        private void dgProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedIndex >= 0 && dgProducts.SelectedIndex < dgProducts.Items.Count)
            {
                Product selectedItem = (Product) dgProducts.SelectedItem;
                lbProductName.Content = selectedItem.Name;
                lbPrice.Content = "$" + selectedItem.Price;
                lbQuantity.Content = selectedItem.Quantity;
                txtDesc.Text = selectedItem.Description;
                lbCategory.Content = selectedItem.Category.Name;
                LoadImageProduct(selectedItem.Image);
            }
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id" || e.PropertyName == "Status" || e.PropertyName == "CategoryId" || e.PropertyName == "Image")
            {
                e.Cancel = true;
            }           
            if (e.PropertyName == "Description")
            {               
                e.Column.Width = new DataGridLength(240);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMimimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Filtering();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            cmbFilter.SelectedIndex = 0;
            LoadData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFilter.SelectedItem != null)
            {
                Filtering();
            }
        }
        private void Filtering()
        {
            dgProducts.ItemsSource = currentProductList.Where(item => item.Name.ToLower().Contains(txtSearch.Text.ToLower())).Where(item => (int)cmbFilter.SelectedValue == 0 || item.CategoryId == (int)cmbFilter.SelectedValue);
        }

        private void LoadImageProduct(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    Uri imageUri = new Uri(imageUrl);
                    imgProduct.Source = new BitmapImage(imageUri);
                    return;
                }
                catch (Exception ex)
                {
                    imgProduct.Source = new BitmapImage(new Uri(NOIMG));
                }
            }
            imgProduct.Source = new BitmapImage(new Uri(NOIMG));
        }
    }    
}
