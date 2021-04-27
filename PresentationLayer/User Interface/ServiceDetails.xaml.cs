using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Window
    {
        public ServiceDetails()
        {
            InitializeComponent();
        }

        private void ReviewButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceReview serviceReview = new ServiceReview();
            serviceReview.Show();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceHistory serviceHistory = new ServiceHistory();
            serviceHistory.Show();
            Close();
        }
    }
}
