using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void CreateAccountButtonClicked(object sender, RoutedEventArgs e)
        {
            Registry registry = new Registry();
            registry.Show();
            Close();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersSearch serviceProvidersSearch = new ServiceProvidersSearch();
            serviceProvidersSearch.Show();
            Close();
        }
    }
}
