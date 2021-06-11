using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para OrdersHistory.xaml
    /// </summary>
    public partial class ServiceHistory : Window
    {
        private DateTime _dateFilter = DateTime.Now;
        private readonly List<BusinessLayer.BusinessEntities.ServiceRequest> _serviceRequests = new List<BusinessLayer.BusinessEntities.ServiceRequest>();

        private void DisplayData()
        {
            //ListViewServices.Items.Clear();
            //foreach (BusinessLayer.BusinessEntities.ServiceRequest serviceRequest in _serviceRequests)
            //{
            //    if(DateTime.Equals(serviceRequest.Date, _dateFilter))
            //    {
            //        ListViewServices.Items.Add(serviceRequest);
            //    }                
            //}
        }

        public ServiceHistory()
        {
            InitializeComponent();            
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                _dateFilter = DatePickerServiceDate.SelectedDate.Value.Date;
                //_dateFilter = _dateFilter.Date;                
                Console.WriteLine(_dateFilter.ToString("yyyy-MM-dd"));
                DisplayData();
            }
            catch (FormatException)
            {
                NotificationWindow.ShowErrorWindow("Fecha no válida", "Por favor, ingrese una fecha con formato válido.");
            }
        }

        private void SeeDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            BusinessLayer.BusinessEntities.ServiceRequest serviceRequest = 
                (BusinessLayer.BusinessEntities.ServiceRequest)ListViewServices.SelectedItem;
            ServiceDetails serviceDetails = new ServiceDetails(serviceRequest);
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
