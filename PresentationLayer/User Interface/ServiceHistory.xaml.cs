using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using System;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para OrdersHistory.xaml
    /// </summary>
    public partial class ServiceHistory : Window
    {
        private DateTime _dateFilter = DateTime.Now;
        private List<ServiceRequestHistoryPresentationModel> _serviceRequests = new List<ServiceRequestHistoryPresentationModel>();

        private void DisplayData()
        {
            ListViewServices.Items.Clear();
            _serviceRequests.ForEach(serviceRequestItem =>
            {
                ListViewServices.Items.Add(serviceRequestItem);
            });
            
        }

        public ServiceHistory()
        {
            InitializeComponent();
            DataContext = _serviceRequests;
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if(DatePickerServiceDate.SelectedDate == null)
                {
                    throw new FormatException();
                }
                _dateFilter = DatePickerServiceDate.SelectedDate.Value.Date;
                BusinessLayer.BusinessEntities.ServiceRequest serviceRequest = new BusinessLayer.BusinessEntities.ServiceRequest();
                Dictionary<string, string> queryParameters = new Dictionary<string, string>
                {
                    ["date"] = _dateFilter.ToString("yyyy-MM-dd")
                };
                List<BusinessLayer.BusinessEntities.ServiceRequest> serviceRequests = serviceRequest.FindByDate(queryParameters);
                _serviceRequests = ServiceRequestMapper.CreateServiceRequestHistoryPresentationModelListFromServiceRequestEntityList(serviceRequests);
                DisplayData();
            }
            catch (FormatException)
            {
                NotificationWindow.ShowErrorWindow("Fecha no válida", "Por favor, ingrese una fecha con formato válido.");
            }
            catch(NetworkRequestException networkRequestException)
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
                        exceptionMessage = "No se encontraron solicitudes de servicio para la fecha indicada.";
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

        private void SeeDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequestHistoryPresentationModel serviceRequest = 
                (ServiceRequestHistoryPresentationModel)ListViewServices.SelectedItem;
            ServiceDetails serviceDetails = new ServiceDetails(serviceRequest.ID);
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
