using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Window
    {
        private ServiceRequestDetailsPresentationModel _serviceRequest;
        private string _serviceRequestID;
        public ServiceDetails(string serviceRequestID)
        {
            InitializeComponent();
            _serviceRequestID = serviceRequestID;
            DataContext = _serviceRequest;            
        }

        private void LoadServiceRequestDetails()
        {
            BusinessLayer.BusinessEntities.ServiceRequest serviceRequest = new BusinessLayer.BusinessEntities.ServiceRequest();
            BusinessLayer.BusinessEntities.ServiceRequest retrievedServiceRequest = serviceRequest.FindByID(_serviceRequestID);
            _serviceRequest = ServiceRequestMapper.CreateServiceRequestDetailsPresentationModelFromServiceRequestEntity(retrievedServiceRequest);
        }

        private void DisplayButtons()
        {
            if (_serviceRequest.Status.Equals("Pendiente de aceptación"))
            {                                
                CreateButton("Cancelar", CancelButtonClicked);
                CreateButton("Calificar", ReviewButtonClicked);
            }
            else if (_serviceRequest.Status.Equals("Completado"))
            {
                CreateButton("Calificar", ReviewButtonClicked);
            }            
        }

        private void CreateButton(string buttonText, RoutedEventHandler eventHandler)
        {
            Button button = new Button
            {
                Width = 120,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10, 0, 10, 0),
                Content = buttonText,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.DarkOrange),
                BorderBrush = new SolidColorBrush(Colors.Transparent),                
            };
            button.Click += eventHandler;
            DockPanelButtons.Children.Add(button);
        }

        private void SetChipColor()
        {
            switch (_serviceRequest.Status)
            {
                case "Pendiente de aceptación":
                    ChipServiceRequestStatus.Background = Brushes.Orange;
                    break;
                case "Completado":
                    ChipServiceRequestStatus.Background = Brushes.ForestGreen;
                    break;
                case "Cancelado":
                    ChipServiceRequestStatus.Background = Brushes.Salmon;
                    break;
                case "Activo":
                    ChipServiceRequestStatus.Background = Brushes.LightBlue;
                    break;
            }
        }

        private void ReviewButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceReview serviceReview = new ServiceReview(_serviceRequest.ServiceProviderID);
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
            ServiceHistory serviceHistory = new ServiceHistory();
            serviceHistory.Show();
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadServiceRequestDetails();
                TextBlockServiceRequestDate.Text = _serviceRequest.Date;
                TextBoxServiceRequestDescription.Text = _serviceRequest.Description;
                TextBlockServiceRequestDeliveryAddress.Text = _serviceRequest.DeliveryAddress;
                TextBlockServiceRequestCost.Text = "" + _serviceRequest.Cost;
                ChipServiceRequestStatus.Content = _serviceRequest.Status;
                SetChipColor();
                DisplayButtons();
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
                        break;
                    case 404:
                        exceptionMessage = "No se encontró la solicitud de servicio.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                }                
            }
        }

        private void GoBackToLoginView()
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
