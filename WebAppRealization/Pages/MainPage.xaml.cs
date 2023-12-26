using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void AdminBrands_Click(object sender, RoutedEventArgs e)
        {
            AdminBrandsListBox.ItemsSource = await new ApiClient().GetUsersAsync();
        }

        private async void Companies_Click(object sender, RoutedEventArgs e)
        {
            CompaniesListBox.ItemsSource = await new ApiClient().GetCompaniesAsync();
        }

        private void AdminBrandsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(AdminBrandsListBox.SelectedItem as UserModel));
        }

        private void CompaniesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(CompaniesListBox.SelectedItem as CompanyModel));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            TokenManager.Token = null;
            NavigationService.GoBack();
        }

        private void AddAdminBrandButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAdminBrandPage());
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAdminBrandPage());
        }
    }
}