using MyShopManagementBO;
using MyShopManagementRepository;
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
using System.Xml.Linq;

namespace MyShopManagementGUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IEmailSender _emailSender; 
        private string _otp; 
        private User _newUser;
        public RegisterWindow()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            _emailSender = new EmailSender();
        }

        private void btnSendOtp_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter email, name, and password!", "Register Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var existingUser = _unitOfWork.UserRepository.Get(email); // Thay FindUserByName bằng Get
            if (existingUser != null)
            {
                MessageBox.Show("Email already exists!", "Register Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _newUser = new User
            {
                Email = email,
                Name = name,
                Address = string.IsNullOrEmpty(address) ? null : address,
                Phone = string.IsNullOrEmpty(phone) ? null : phone,
                Password = password,
                RoleId = 1,        // Default role: Customer
                Enabled = false    // Will be enabled after OTP verification
            };

            _unitOfWork.UserRepository.Create(_newUser); // Thay Add bằng Create
            _unitOfWork.SaveChange();

            _otp = _emailSender.GetOTP();
            bool emailSent = _emailSender.SendEmail(email, "Your OTP for Registration", $"Your OTP is: {_otp}");

            if (!emailSent)
            {
                MessageBox.Show("Failed to send OTP. Please check your email address!", "Register Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                _unitOfWork.UserRepository.Delete(_newUser.Email); // Thay Delete(User) bằng Delete(string email)
                _unitOfWork.SaveChange();
                return;
            }

            lblOtp.Visibility = Visibility.Visible;
            txtOtp.Visibility = Visibility.Visible;
            btnSendOtp.Visibility = Visibility.Collapsed;
            btnRegister.Visibility = Visibility.Visible;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string otpInput = txtOtp.Text.Trim();

            if (string.IsNullOrEmpty(otpInput))
            {
                MessageBox.Show("Please enter OTP!", "Register Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (otpInput == _otp)
            {
                _newUser.Enabled = true;
                _unitOfWork.UserRepository.Update(_newUser);
                _unitOfWork.SaveChange();
                MessageBox.Show("Register successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong OTP!", "Register Fail", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
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
    }
}



