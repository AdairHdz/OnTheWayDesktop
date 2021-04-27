using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para OrdersHistory.xaml
    /// </summary>
    public partial class ServiceHistory : Window
    {
        public ServiceHistory()
        {
            InitializeComponent();
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SeeDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceDetails serviceDetails = new ServiceDetails();
            serviceDetails.Show();
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }
    }
}
