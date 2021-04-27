using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Window
    {
        private readonly BusinessLayer.BusinessEntities.ServiceRequest _serviceRequest;
        public ServiceDetails(BusinessLayer.BusinessEntities.ServiceRequest serviceRequest)
        {
            InitializeComponent();
            _serviceRequest = serviceRequest;
            this.DataContext = _serviceRequest;
            SetChipColor();
            DisplayButtons();
        }

        private void DisplayButtons()
        {
            if(_serviceRequest.ServiceStatus == BusinessLayer.BusinessEntities.ServiceStatus.PendingOfAcceptance)
            {
                ButtonCancel.Visibility = Visibility.Visible;
                ButtonReview.Visibility = Visibility.Hidden;
            }else if(_serviceRequest.ServiceStatus == BusinessLayer.BusinessEntities.ServiceStatus.Concretized)
            {
                ButtonCancel.Visibility = Visibility.Hidden;
                ButtonReview.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonReview.Visibility = Visibility.Hidden;
                ButtonCancel.Visibility = Visibility.Hidden;
            }
        }

        private void SetChipColor()
        {
            switch (_serviceRequest.ServiceStatus)
            {
                case BusinessLayer.BusinessEntities.ServiceStatus.PendingOfAcceptance:
                    ChipServiceRequestStatus.Background = Brushes.Orange;
                    break;
                case BusinessLayer.BusinessEntities.ServiceStatus.Concretized:
                    ChipServiceRequestStatus.Background = Brushes.ForestGreen;
                    break;
                case BusinessLayer.BusinessEntities.ServiceStatus.Canceled:
                    ChipServiceRequestStatus.Background = Brushes.Salmon;
                    break;
                case BusinessLayer.BusinessEntities.ServiceStatus.Active:
                    ChipServiceRequestStatus.Background = Brushes.LightBlue;
                    break;                
            }            
        }

        private void ReviewButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceReview serviceReview = new ServiceReview(_serviceRequest);
            serviceReview.Show();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceHistory serviceHistory = new ServiceHistory();
            serviceHistory.Show();
            Close();
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
