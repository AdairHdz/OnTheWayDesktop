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
        private readonly List<BusinessLayer.BusinessEntities.ServiceRequest> _serviceRequests = new List<BusinessLayer.BusinessEntities.ServiceRequest>
            {
                new BusinessLayer.BusinessEntities.ServiceRequest
                {
                    KindOfService = BusinessLayer.BusinessEntities.KindOfService.Delivery,
                    Cost = 80,
                    ServiceStatus = BusinessLayer.BusinessEntities.ServiceStatus.Active,
                    ServiceProvider = new BusinessLayer.BusinessEntities.ServiceProvider
                    {
                        Names = "Pedro",
                        Lastname = "Pérez"
                    },
                    Date = new DateTime(2021, 08, 20),
                    Description = "Necesito que entregue un pastel en 1ra de Morelos #22, Colonia Centro. Pase a recoger el pastel a mi dirección",
                    DeliveryAddress = new BusinessLayer.BusinessEntities.Address
                    {
                        IndoorNumber = "",
                        OutdoorNumber = "",
                        Street = "Insurgentes",
                        Suburb = "Primaveras",
                    }
                },
                new BusinessLayer.BusinessEntities.ServiceRequest
                {
                    KindOfService = BusinessLayer.BusinessEntities.KindOfService.DrugPurchase,
                    Cost = 50,
                    ServiceStatus = BusinessLayer.BusinessEntities.ServiceStatus.Canceled,
                    ServiceProvider = new BusinessLayer.BusinessEntities.ServiceProvider
                    {
                        Names = "Ricardo",
                        Lastname = "Palacios"
                    },
                    Date = new DateTime(2021, 08, 20),
                    Description = "1 caja de ibuprofeno y 2 curitas",
                    DeliveryAddress = new BusinessLayer.BusinessEntities.Address
                    {
                        IndoorNumber = "",
                        OutdoorNumber = "",
                        Street = "Insurgentes",
                        Suburb = "Primaveras",
                    }
                },
                new BusinessLayer.BusinessEntities.ServiceRequest
                {
                    KindOfService = BusinessLayer.BusinessEntities.KindOfService.Delivery,
                    Cost = 40,
                    ServiceStatus = BusinessLayer.BusinessEntities.ServiceStatus.Concretized,
                    ServiceProvider = new BusinessLayer.BusinessEntities.ServiceProvider
                    {
                        Names = "Juan",
                        Lastname = "Gutiérrez"
                    },
                    Date = new DateTime(2021, 08, 21),
                    Description = "Necesito que entregue una blusa y una bolsa en el crucero. Por favor recoja las prendas en mi dirección",
                    DeliveryAddress = new BusinessLayer.BusinessEntities.Address
                    {
                        IndoorNumber = "",
                        OutdoorNumber = "",
                        Street = "Insurgentes",
                        Suburb = "Primaveras",
                    }
                },
                new BusinessLayer.BusinessEntities.ServiceRequest
                {
                    KindOfService = BusinessLayer.BusinessEntities.KindOfService.GroceryShopping,
                    Cost = 100,
                    ServiceStatus = BusinessLayer.BusinessEntities.ServiceStatus.PendingOfAcceptance,
                    ServiceProvider = new BusinessLayer.BusinessEntities.ServiceProvider
                    {
                        Names = "Adair",
                        Lastname = "Hernández"
                    },
                    Date = new DateTime(2021, 08, 21),
                    Description = "6 cajas de leche Lala y 1kg de manzanas",
                    DeliveryAddress = new BusinessLayer.BusinessEntities.Address
                    {
                        IndoorNumber = "",
                        OutdoorNumber = "",
                        Street = "Insurgentes",
                        Suburb = "Primaveras",
                    }
                }

            };

        private void DisplayData()
        {
            ListViewServices.Items.Clear();
            foreach (BusinessLayer.BusinessEntities.ServiceRequest serviceRequest in _serviceRequests)
            {
                if(DateTime.Equals(serviceRequest.Date, _dateFilter))
                {
                    ListViewServices.Items.Add(serviceRequest);
                }                
            }
        }

        public ServiceHistory()
        {
            InitializeComponent();            
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                _dateFilter = DatePickerServiceDate.SelectedDate.Value;
                _dateFilter = _dateFilter.Date;
                DisplayData();
            }
            catch (FormatException)
            {

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
