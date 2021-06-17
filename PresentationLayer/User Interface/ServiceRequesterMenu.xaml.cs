using BusinessLayer.BusinessEntities;
using PresentationLayer.Helpers;
using System;
using System.Windows;
using Utils.CustomExceptions;

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
            User user = new User();            
            Login login = new Login();
            try
            {
                user.Logout();
            }
            catch (NetworkRequestException)
            {
                NotificationWindow.ShowNotificationWindow("Cierre de sesión", "Cerrando sesión...");
            }
            finally
            {                
                login.Show();
                Close();
            }            
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

        private void SeeStatisticsButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequesterStatistics serviceRequesterStatistics = new ServiceRequesterStatistics();
            serviceRequesterStatistics.Show();
            Close();
        }
    }
}