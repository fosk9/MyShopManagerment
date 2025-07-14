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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUserService userService = new UserService();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text.Trim().ToLower();
            string password = txtPassword.Password.ToString();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter email!", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter password!", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var user = userService.Get(email);

            if (user == null)
            {
                MessageBox.Show("Email does not match any account!", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (user.Password != password)
            {
                MessageBox.Show("Wrong email or password!", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (user.Enabled == false)
            {
                MessageBox.Show("You have no permission to access system", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            switch (user.RoleId)
            {
                case 1:
                    CustomerWindow customerWindow = new CustomerWindow(user);
                    customerWindow.Show();
                    this.Close();
                    break;
                case 2:
                    ManagerWindow managerWindow = new ManagerWindow(user);
                    managerWindow.Show();
                    this.Close();
                    break;
                case 3:
                    AdminWindow adminWindow = new AdminWindow(user);
                    adminWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnMimimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}

