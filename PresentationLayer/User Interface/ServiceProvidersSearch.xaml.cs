using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
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
        private ServiceProviderOverviewPaginationPresentationModel _serviceProviders;
        private int _page = 1;
        private int _pagesize;
        public ServiceProvidersSearch()
        {
            InitializeComponent();
            _serviceProviders = new ServiceProviderOverviewPaginationPresentationModel();
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
            } else
            {
                SeeDetailsButton.IsEnabled = false;
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
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
                        break;
                    case 404:
                        exceptionMessage = "No se encontraron coincidencias para la información solicitada. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;                    
                    case 500:
                        exceptionMessage = "Ha ocurrido un error interno en el servidor. Por favor, intente más tarde.";
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

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {                
                if(_pagesize == 0)
                {
                    throw new ArgumentNullException();
                }
                LoadServiceProviders();
            }
            catch(ArgumentNullException)
            {
                NotificationWindow.ShowErrorWindow("Error", "Por favor seleccione el número de página.");
            }            
        }

        private void LoadServiceProviders()
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
            queryParameters["page"] = _page.ToString();
            queryParameters["pagesize"] = _pagesize.ToString();
            try
            {
                ServiceProviderPaginationDTO serviceProviderPaginationDTO = serviceProvider.FindMatches(queryParameters);
                _serviceProviders = ServiceProviderMapper.CreateListOfServiceProviderOverviewPagination(serviceProviderPaginationDTO);
                _serviceProviders.ServiceProvidersOverview.ForEach(serviceProviderModel =>
                {
                    ServiceProvidersListView.Items.Add(serviceProviderModel);
                });

                CurrentPageButton.Content = _serviceProviders.Page;
                if (_serviceProviders.Page > 1)
                {
                    StartingPageButton.IsEnabled = true;
                    PreviousPageButton.IsEnabled = true;
                } else
                {
                    StartingPageButton.IsEnabled = false;
                    PreviousPageButton.IsEnabled = false;
                }
                

                if((_serviceProviders.Page * _serviceProviders.PerPage) - _serviceProviders.Total < 0) {
                    LastPageButton.IsEnabled = true;
                    NextPageButton.IsEnabled = true;
                }
                else
                {
                    LastPageButton.IsEnabled = false;
                    NextPageButton.IsEnabled = false;
                }
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage = "";
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Por favor asegúrese de haber proporcionado parámetros válidos de búsqueda.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha caducado de forma inesperada.";
                        GoBackToLoginView();
                        break;
                    case 404:
                        exceptionMessage = "No se encontraron proveedores de servicio para los parámetros de búsqueda especificados.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    case 409:
                        exceptionMessage = "Ocurrió un problema al intentar recuperar la información solicitada. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al momento de procesar la solicitud. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    default:
                        exceptionMessage = "Ocurrió un error desconocido. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                }
                
            }
        }


        private void NotifyErrorAndDisableButtons(string exceptionMessage)
        {
            NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            StartingPageButton.IsEnabled = false;
            PreviousPageButton.IsEnabled = false;
            LastPageButton.IsEnabled = false;
            NextPageButton.IsEnabled = false;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadCities();
        }

        private void FirstPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page = 1;
            LoadServiceProviders();
        }

        private void PreviousPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page--;
            LoadServiceProviders();
        }

        private void NextPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page++;
            LoadServiceProviders();
        }

        private void LastPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page = _serviceProviders.Pages;
            LoadServiceProviders();
        }

        private void PageSizeComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            _pagesize = int.Parse(((ComboBoxItem)ComboBoxPageSize.SelectedItem).Tag.ToString());
        }
    }
}
