using System.Windows;
using Utils;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceRequesterMenu.xaml
    /// </summary>
    public partial class ServiceRequesterMenu : Window
    {
        public ServiceRequesterMenu()
        {
            InitializeComponent();
        }

        private void LogOutButtonClicked(object sender, RoutedEventArgs e)
        {
            Session.DeleteSession();
            Login login = new Login();
            login.Show();
            Close();
        }

        private void SeeServiceProvidersButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersSearch serviceProvidersSearch = new ServiceProvidersSearch();
            serviceProvidersSearch.Show();
            Close();
        }

        private void SeeServiceHistoryButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceHistory serviceHistory = new ServiceHistory();
            serviceHistory.Show();
            Close();
        }

        private void RegisterAddressButtonClicked(object sender, RoutedEventArgs e)
        {
            AddressRegistry addressRegistry = new AddressRegistry();
            addressRegistry.Show();
            Close();
        }
    }
}