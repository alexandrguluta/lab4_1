using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class SelectedItemListBox : Page
    {
        private UserModel? UserModel;
        private CompanyModel? companyModel;
        public SelectedItemListBox(UserModel? _UserModel)
        {
            InitializeComponent();
            UserModel = _UserModel;
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Number"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("EmployeeNumber"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            //FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            //vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Model"));
            //stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить Пользовательа и добавить администратора";
            DeleteButton.Content = "Удалить Пользователя";
            DeleteAdminModelOrEmployeeButton.Content = "Удалить выбранного администратора";
        }
        public SelectedItemListBox(CompanyModel? _companyModel)
        {
            InitializeComponent();
            companyModel = _companyModel;
            
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Age"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Position"));
            stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить информацию о компании";
            DeleteButton.Content = "Удалить Компанию";
            DeleteAdminModelOrEmployeeButton.Content = "Удалить выбранного сотрудника";
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                listBox.ItemsSource = await new ApiClient().GetEmployeesByCompanyAsync(companyModel.Id);
            }
            else
            {
                listBox.ItemsSource = await new ApiClient().GetAdminsByUserAsync(UserModel.Id);
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                await new ApiClient().DeleteUserdAsync(companyModel.Id);
                
                NavigationService?.GoBack();
            }
            else
            {
                await new ApiClient().DeleteUserdAsync(UserModel.Id);
                
                NavigationService?.GoBack();
            }
        }

      

        private async void DeleteAdminModelOrEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                var selectedItem = listBox.SelectedItem as EmployeeModel;
            }
            else
            {
                var selectedItem = listBox.SelectedItem as AdminModel;
                
                await new ApiClient().DeleteAdminAsync(selectedItem.Id, UserModel.Id);
            }
            
            
        }
    }
}