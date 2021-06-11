using PresentationLayer.PresentationModels;
using System.Windows;
using System.Windows.Media;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Window
    {
        //private readonly ServiceRequestHistoryPresentationModel _serviceRequest;
        public ServiceDetails(string serviceRequestID)
        {
            InitializeComponent();
            //_serviceRequest = serviceRequest;
            //DataContext = _serviceRequest;
            SetChipColor();
            DisplayButtons();
        }

        private void DisplayButtons()
        {
            //if(_serviceRequest.Status.Equals("Pendiente de aceptación"))
            //{
            //    ButtonCancel.Visibility = Visibility.Visible;
            //    ButtonReview.Visibility = Visibility.Hidden;
            //}
            //else if(_serviceRequest.Status.Equals("Completado"))
            //{
            //    ButtonCancel.Visibility = Visibility.Hidden;
            //    ButtonReview.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    ButtonReview.Visibility = Visibility.Hidden;
            //    ButtonCancel.Visibility = Visibility.Hidden;
            //}
        }

        private void SetChipColor()
        {
            //switch (_serviceRequest.Status)
            //{
            //    case "Pendiente de aceptación":
            //        ChipServiceRequestStatus.Background = Brushes.Orange;
            //        break;
            //    case "Completado":
            //        ChipServiceRequestStatus.Background = Brushes.ForestGreen;
            //        break;
            //    case "Cancelado":
            //        ChipServiceRequestStatus.Background = Brushes.Salmon;
            //        break;
            //    case "Activo":
            //        ChipServiceRequestStatus.Background = Brushes.LightBlue;
            //        break;        
            //}            
        }

        private void ReviewButtonClicked(object sender, RoutedEventArgs e)
        {
            //ServiceReview serviceReview = new ServiceReview(_serviceRequest);
            //serviceReview.Show();
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
