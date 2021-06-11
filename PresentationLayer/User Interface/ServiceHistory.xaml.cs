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
            catch(NetworkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Fecha no válida", "Error.");
            }
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
