using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceProviderDetails.xaml
    /// </summary>
    public partial class ServiceProviderDetails : Window
    {
        public ServiceProviderDetails()
        {
            InitializeComponent();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersSearch serviceProvidersSearch = new ServiceProvidersSearch();
            serviceProvidersSearch.Show();
            Close();
        }

        private void RequestServiceButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequest serviceRequest = new ServiceRequest();
            serviceRequest.Show();
        }
    }
}
