using MyShopManagementBO;
using MyShopManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace MyShopManagementGUI
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private User currentUser;
        private readonly IProductService productService = new ProductService();
        private OperatorMode mode;
        private List<Product> currentProductList;
        private const string NOIMG = "https://t3.ftcdn.net/jpg/04/34/72/82/360_F_434728286_OWQQvAFoXZLdGHlObozsolNeuSxhpr84.jpg";
        public ManagerWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            userProfileStackPanel.DataContext = user;
            txtWelcome.Text = "Welcome manager, " + user.Name + "!";
            Init();
            //MessageBox.Show("Welcome to mode for manager!", "Login Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Init()
        {
            UpdateOperatorMode(OperatorMode.Add);
            ResetInfoToEmpty();
            LoadData();
            cmbProductCategory.ItemsSource = productService.GetCategories();
            cmbProductCategory.SelectedIndex = 0;
            var cmbFilterSources = productService.GetCategories();
            cmbFilterSources.Insert(0, new Category() { Id = 0, Name = "All" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.ItemsSource = cmbFilterSources;
        }

        private void LoadData()
        {
            try
            {
                if (dgProducts.Items.Count == 0)
                    UpdateOperatorMode(OperatorMode.Add);
                int index = dgProducts.SelectedIndex;
                currentProductList = productService.GetAll().ToList();
                dgProducts.ItemsSource = currentProductList;
                dgProducts.SelectedIndex = mode == OperatorMode.Add ? -1 : Math.Min(index, dgProducts.Items.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not load data: " + ex.Message, "Something is wrong!");
            }            
        }

        private void UpdateOperatorMode(OperatorMode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case OperatorMode.Add:
                    btnAdd.IsEnabled = true;
                    btnUpdate.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    dgProducts.SelectedIndex = -1;
                    lbMode.Content = "Adding Mode";
                    lbMode.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
                case OperatorMode.Update:
                    btnAdd.IsEnabled = false;
                    btnUpdate.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    lbMode.Background = new SolidColorBrush(Colors.LightBlue);
                    lbMode.Content = "Updating Mode";
                    break;
            }
        }

        private void dgProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedIndex >= 0 && dgProducts.SelectedIndex < dgProducts.Items.Count)
            {
                UpdateOperatorMode(OperatorMode.Update);
                Product selectedItem = (Product)dgProducts.SelectedItem;
                txtProductName.Text = selectedItem.Name;
                txtDescription.Text = selectedItem.Description;
                txtPrice.Text = selectedItem.Price.ToString();
                txtQuantity.Text = selectedItem.Quantity.ToString();
                chkStatus.IsChecked = selectedItem.Status;
                cmbProductCategory.SelectedValue = selectedItem.CategoryId;
                LoadImageProduct(selectedItem.Image);
                txtImgProduct.Text = selectedItem.Image;
            }
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id" || e.PropertyName == "CategoryId")
            {
                e.Cancel = true;
            }
            if (e.PropertyName == "Description")
            {
                e.Column.Width = new DataGridLength(200);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var target = getTargetObject();
                productService.Create(target);
                LoadData();
                ResetInfoToEmpty();
                MessageBox.Show("Added the product successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to add new!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var target = getTargetObject();
                productService.Update(target);
                LoadData();
                MessageBox.Show("Updated the product successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to update!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    productService.Delete(getCurrentSelectedItemID());
                    LoadData();                    
                    MessageBox.Show("Delete the product successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to delete!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            UpdateOperatorMode(OperatorMode.Add);
            ResetInfoToEmpty();
        }

        private void ResetInfoToEmpty()
        {
            txtProductName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtPrice.Text = "";
            txtQuantity.Text = "";
            chkStatus.IsChecked = true;
            cmbProductCategory.SelectedIndex = 0;
            LoadImageProduct(NOIMG);
            txtImgProduct.Text = string.Empty;
        }

        private int getCurrentSelectedItemID()
        {
            var product = (Product)dgProducts.SelectedItem;
            return product == null ? 0 : product.Id;
        }
        private Product getTargetObject()
        {
            if (txtProductName.Text.Length == 0)
                throw new Exception("Name is required!");
            if (txtPrice.Text.Trim().Length == 0)
                throw new Exception("Price is required!");
            if (txtQuantity.Text.Trim().Length == 0)
                throw new Exception("Quantity is required!");           
            
            var target = new Product
            {
                Id = mode == OperatorMode.Add ? 0 : getCurrentSelectedItemID(),
                Name = txtProductName.Text.Trim(),
                Description = txtDescription.Text,
                Price = float.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text),
                Status = chkStatus.IsChecked == true,
                CategoryId = (int) cmbProductCategory.SelectedValue,
                Image = txtImgProduct.Text,
            };

            return target;
        }
        private void txtInteger_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void txtDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = (sender as TextBox)?.Text + e.Text;

            if (!float.TryParse(newText + '0', out _))
            {
                e.Handled = true;
            }
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

        private void txtImgProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string imageUrl = txtImgProduct.Text;

            LoadImageProduct(imageUrl);
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

        private enum OperatorMode
        {
            Add,
            Update
        }
    }
}
