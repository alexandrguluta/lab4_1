using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote;

namespace WebAppIImpl.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            
            var authApiClient = new ApiClient();
            var token = await authApiClient.LoginUserAsync(username, password);
            
            if (token != null)
            {
                TokenManager.Token = token;
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Ошибка: Вход не выполнен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}