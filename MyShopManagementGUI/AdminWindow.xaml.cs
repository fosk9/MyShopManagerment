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

namespace MyShopManagementGUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private User currentUser;
        private readonly IUserService userService = new UserService();
        private OperatorMode mode;
        private List<User> currentUserList;
        public AdminWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            userProfileStackPanel.DataContext = user;
            txtWelcome.Text = "Welcome admin, " + user.Name + "!";
            Init();
            //MessageBox.Show("Welcome to mode for admin!", "Login Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Init()
        {
            UpdateOperatorMode(OperatorMode.Add);
            LoadData();
            ResetInfoToEmpty();
            cmbUserRole.ItemsSource = userService.GetRoles().Where(item => item.Name.ToLower() != "admin");
            cmbUserRole.SelectedIndex = 0;

            var cmbFilterSources = userService.GetRoles().Where(item => item.Name.ToLower() != "admin").ToList();
            cmbFilterSources.Insert(0, new Role() { Id = 0, Name = "All" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.ItemsSource = cmbFilterSources;
        }

        private void LoadData()
        {
            try
            {
                if (dgUsers.Items.Count == 0)
                    UpdateOperatorMode(OperatorMode.Add);
                int index = dgUsers.SelectedIndex;
                currentUserList = userService.GetAll()
                .Where(item => item.Role != null && !string.IsNullOrEmpty(item.Role.Name) && item.Role.Name.ToLower() != "admin")
                .ToList();
                dgUsers.ItemsSource = currentUserList;
                dgUsers.SelectedIndex = mode == OperatorMode.Add ? -1 : Math.Min(index, dgUsers.Items.Count - 1);
                // Reapply any active filter after loading full data
                Filtering();
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
                    txtEmail.IsReadOnly = false;
                    dgUsers.SelectedIndex = -1;
                    lbMode.Content = "Adding Mode";
                    lbMode.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
                case OperatorMode.Update:
                    btnAdd.IsEnabled = false;
                    btnUpdate.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    txtEmail.IsReadOnly = true;
                    lbMode.Background = new SolidColorBrush(Colors.LightBlue);
                    lbMode.Content = "Updating Mode";
                    break;
            }
        }

        private void dgUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedIndex >= 0 && dgUsers.SelectedIndex < dgUsers.Items.Count)
            {
                UpdateOperatorMode(OperatorMode.Update);
                User selectedItem = (User)dgUsers.SelectedItem;
                txtEmail.Text = selectedItem.Email;
                txtName.Text = selectedItem.Name;
                txtPassword.Password = ""; // Do not display the existing password for security reasons
                txtPhone.Text = selectedItem.Phone;
                txtAddress.Text = selectedItem.Address;
                chkStatus.IsChecked = selectedItem.Enabled;
                cmbUserRole.SelectedValue = selectedItem.RoleId;
            }
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Password" || e.PropertyName == "RoleId")
            {
                e.Cancel = true;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var target = getTargetObject();
                userService.Create(target);
                LoadData();
                ResetInfoToEmpty();
                MessageBox.Show("Added the user successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
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
                userService.Update(target);
                LoadData();
                MessageBox.Show("Updated the user successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    userService.Delete(getCurrentSelectedItemID());
                    LoadData();
                    MessageBox.Show("Delete the user successfully!", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
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
            txtEmail.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPassword.Password = String.Empty;
            txtPhone.Text = String.Empty;
            txtAddress.Text = String.Empty;
            chkStatus.IsChecked = true;
            cmbUserRole.SelectedIndex = 0;
        }

        private string getCurrentSelectedItemID()
        {
            return txtEmail.Text.Trim().ToLower();
        }
        private User getTargetObject()
        {
            if (txtEmail.Text.Trim().Length == 0)
                throw new Exception("Email is required!");
            if (ValidateEmail(txtEmail.Text.Trim()) == false)
                throw new Exception("Email is invalid format!");

            if (txtName.Text.Trim().Length == 0)
                throw new Exception("Name is required!");
            if (txtPhone.Text.Length > 0 && ValidatePhoneNumber(txtPhone.Text) == false)
                throw new Exception("Phone number is invalid format!");

            User target;
            string newPassword = txtPassword.Password.Trim();

            if (mode == OperatorMode.Add)
            {
                if (string.IsNullOrEmpty(newPassword))
                    throw new Exception("Password is required & not allow for blank string!");
                target = new User();
                target.Password = newPassword;
            }
            else
            {
                // For update, fetch the existing user to preserve unchanged fields (e.g., password)
                target = userService.Get(getCurrentSelectedItemID());
                if (!string.IsNullOrEmpty(newPassword))
                {
                    target.Password = newPassword; // Update password only if a new one is provided
                }
                // Else, keep the existing password
            }

            target.Email = txtEmail.Text.Trim().ToLower();
            target.Name = txtName.Text;
            target.Phone = txtPhone.Text;
            target.Address = txtAddress.Text;
            target.RoleId = (int)cmbUserRole.SelectedValue;
            target.Enabled = chkStatus.IsChecked == true;

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

        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^((\+84)|0)(3[2-9]|5[2689]|7[06789]|8[1-9]|9[0-9])(\d{7})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        private static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            Match match = Regex.Match(email, pattern);

            return match.Success;
        }

        private enum OperatorMode
        {
            Add,
            Update
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
            var filteredList = currentUserList
                .Where(item => item.Email.ToLower().Contains(txtSearch.Text.ToLower()) || item.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                .Where(item => (int)cmbFilter.SelectedValue == 0 || item.RoleId == (int)cmbFilter.SelectedValue);
            dgUsers.ItemsSource = filteredList;
        }
    }
}