using System.Windows;
using WebAppIImpl.Pages;

namespace WebAppIImpl
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new LoginPage());
        }
    }
}
