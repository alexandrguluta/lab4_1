using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class AddAdminBrandPage : Page
    {
        private ObservableCollection<AdminModel> AdminModels = new ();
        private UserModel? User = null;
        
        public AddAdminBrandPage()
        {
            InitializeComponent();
        }
        
        public AddAdminBrandPage(UserModel UserModel)
        {
            InitializeComponent();
            User = UserModel;

            NameTextBox.Text = User.Name;
            CountryManifactureTextBox.Text = User.Address;

            ButtonAction.Content = "Обновить";
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AdminModels.Add(new AdminModel
            {
                Number = PhoneAdminModelTextBox.Text,
                EmployeeNumber = Int32.Parse(NumberTextBox.Text)
            });

            if (User == null)
            {
                var item = new UserCreationModel()
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Admins = AdminModels
                };
                if (await new ApiClient().PostCreateUserAsync(item) == null)
                {
                    MessageBox.Show("Ошибка: Пользователь не добавлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Пользователь добавлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                var item = new UserCreationModel
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Admins = AdminModels
                };
                if (await new ApiClient().PutUpdateUserdAsync(item, User.Id) != null)
                {
                    MessageBox.Show("Ошибка: Пользователь не обновлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Пользователь Обновлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void CreateAdminModelButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberTextBox.Text == "" || PhoneAdminModelTextBox.Text == "") return;
            
            var item = new AdminCreationModel
            {
                EmployeeNumber = Int32.Parse(NumberTextBox.Text),
                Number = PhoneAdminModelTextBox.Text,
            };

            if(await new ApiClient().PostAdminForUserAsync(item, User.Id) != null)
            {
                MessageBox.Show("Самолёт добавлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Свмолёт не добавлен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            NumberTextBox.Text = "";
            PhoneAdminModelTextBox.Text = "";
        }
    }
}