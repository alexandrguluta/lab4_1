using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var registrationModel = new RegistrationModel
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                UserName = UserNameTextBox.Text,
                Password = PasswordBox.Password,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Roles = new Collection<string> {RoleTextBox.Text}
            };

            var authApiClient = new ApiClient();
            var token = await authApiClient.RegistrationUserAsync(registrationModel);
            
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

        private void ButtonBase_OnClickButton_Click(object sender, RoutedEventArgs e)
        { 
            NavigationService.GoBack();
        }
    }
}