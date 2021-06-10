using BusinessLayer.BusinessEntities;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utils;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceProvidersSearch.xaml
    /// </summary>
    public partial class ServiceProvidersSearch : Window
    {
        private List<ServiceProviderOverviewItemPresentationModel> _serviceProviders;
        public ServiceProvidersSearch()
        {
            InitializeComponent();
            _serviceProviders = new List<ServiceProviderOverviewItemPresentationModel>();
        }

        private void MaxPriceSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double maxPriceSliderValue = MaxPriceSlider.Value;          
            MaxPriceText.Text = $"{Math.Round(maxPriceSliderValue)}.00 MXN";
        }

        private void ServiceProvidersListViewSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(ServiceProvidersListView.SelectedItem != null)
            {
                SeeDetailsButton.IsEnabled = true;
            }
        }

        private void SeeDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            var selectedServiceProvider =
                (ServiceProviderOverviewItemPresentationModel)ServiceProvidersListView.SelectedItem;
            ServiceProviderDetails serviceProviderDetails = new ServiceProviderDetails(selectedServiceProvider.ID);
            serviceProviderDetails.Show();
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }

        private void LoadCities()
        {
            try
            {
                ComboBoxCity.Items.Clear();
                Session session = Session.GetSession();
                City city = new City();
                List<City> cities = city.GetCities(session.StateID);
                List<CityPresentationModel> cityPresentationModels =
                    CityMapper.CreateListOfCityPresentationModels(cities);

                cityPresentationModels.ForEach(cityPresentationModel =>
                {
                    ComboBoxCity.Items.Add(cityPresentationModel);
                });
            }
            catch (NetworkRequestException networkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                NavigateToServiceRequesterMenu();
            }
            catch (EmptyCollectionException)
            {
                NotificationWindow.ShowNotificationWindow("Sin coincidencias", "Actualmente OnTheWay no cuenta con ciudades registradas en su Estado.");
                NavigateToServiceRequesterMenu();
            }
            
        }

        private void NavigateToServiceRequesterMenu()
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersListView.Items.Clear();
            ServiceProvider serviceProvider = new ServiceProvider();
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();

            string cityName = ComboBoxCity.Text;
            string kindOfService = ((ComboBoxItem)ComboBoxKindOfService.SelectedItem).Tag.ToString();
            string maxPriceRate = Math.Round(MaxPriceSlider.Value).ToString();
            queryParameters["maxPriceRate"] = maxPriceRate;
            queryParameters["city"] = cityName;
            queryParameters["kindOfService"] = kindOfService;
            queryParameters["page"] = "1";
            queryParameters["pagesize"] = "10";
            try
            {
                _serviceProviders = ServiceProviderMapper.CreateListOfServiceProviderOverviewItemDTO(serviceProvider.FindMatches(queryParameters));
                _serviceProviders.ForEach(serviceProviderModel =>
                {
                    ServiceProvidersListView.Items.Add(serviceProviderModel);
                });
            }
            catch(NetworkRequestException networkRequestException)
            {
                string exceptionMessage = "";
                switch(networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Por favor asegúrese de haber proporcionado parámetros válidos de búsqueda.";
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha caducado de forma inesperada.";
                        break;
                    case 404:
                        exceptionMessage = "No se encontraron proveedores de servicio para los parámetros de búsqueda especificados.";
                        break;
                    case 409:
                        exceptionMessage = "Ocurrió un problema al intentar recuperar la información solicitada. Por favor, intente más tarde.";
                        break;
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al momento de procesar la solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ocurrió un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }
                      
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadCities();
        }
    }
}
