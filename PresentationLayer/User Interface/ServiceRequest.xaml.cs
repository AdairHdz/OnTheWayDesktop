using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utils;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceRequest.xaml
    /// </summary>
    public partial class ServiceRequest : Window
    {
        private string _serviceProviderID;
        private ServiceRequestPresentationModel _serviceRequestPresentationModel = new ServiceRequestPresentationModel();
        public ServiceRequest(string serviceProviderID)
        {
            _serviceProviderID = serviceProviderID;
            DataContext = _serviceRequestPresentationModel;
            InitializeComponent();
        }

        private void LoadAddresses() {
            try
            {
                Address address = new Address();
                List<Address> addresses = address.FindAll();
                List<AddressPresentationModel> addressPresentationModels = AddressMapper.CreateAddressPresentationModelList(addresses);
                addressPresentationModels.ForEach(addressItem =>
                {
                    ComboBoxAddress.Items.Add(addressItem);
                });
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        break;
                    case 404:
                        exceptionMessage = "No tiene direcciones registradas. Por favor, registre una dirección antes de solicitar un servicio.";
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }            
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
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        break;
                    case 404:
                        exceptionMessage = "Ocurrió un error al intentar recuperar las ciudades disponibles para OnTheWay. Por favor, intente más tarde.";
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }            
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAddresses();
                LoadCities();
            }
            catch(NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        break;
                    case 404:
                        exceptionMessage = "No tiene direcciones registradas. Por favor, registre una dirección antes de solicitar un servicio.";
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }
        }

        private void SendServiceRequestButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequestPresentationModelValidator serviceRequestPresentationModelValidator = new ServiceRequestPresentationModelValidator();
            FluentValidation.Results.ValidationResult validationResult = serviceRequestPresentationModelValidator.Validate(_serviceRequestPresentationModel);
            _serviceRequestPresentationModel.ServiceRequesterID = Session.GetSession().ID;
            _serviceRequestPresentationModel.ServiceProviderID = _serviceProviderID;
            ShowFeedback(validationResult);
            if (validationResult.IsValid)
            {
                if (_serviceRequestPresentationModel.Cost > 0)
                {
                    SendServiceRequest();
                }
                else
                {
                    NotificationWindow.ShowErrorWindow("No hay tarifa una activa", "El proveedor de servicios no posee una tarifa activa en el horario de su solicitud.");
                }
            }
            else
            {
                NotificationWindow.ShowErrorWindow("Error", "Por favor asegúrese de haber introducido los datos solicitados e intente nuevamente.");
            }
        }

        private void SendServiceRequest()
        {
            try
            {
                BusinessLayer.BusinessEntities.ServiceRequest serviceRequest = ServiceRequestMapper.CreateServiceRequestEntityFromServiceRequestPresentationModel(_serviceRequestPresentationModel);
                serviceRequest.Save();
                NotificationWindow.ShowNotificationWindow("Solicitud enviada", "Se ha enviado su solicitud de servicio.");
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        break;                    
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }
        }

        private void ShowFeedback(FluentValidation.Results.ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(FormGrid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void LoadActivePriceRate()
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            queryParameters["kindOfService"] = "" + _serviceRequestPresentationModel.KindOfService;
            queryParameters["city"] = _serviceRequestPresentationModel.City != null ? _serviceRequestPresentationModel.City.Name : "";
            PriceRate priceRate = new PriceRate();
            try
            {
                PriceRate activePriceRate = priceRate.GetActivePriceRate(_serviceProviderID, queryParameters);
                _serviceRequestPresentationModel.Cost = activePriceRate.Price;                
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
                    case 404:                        
                        _serviceRequestPresentationModel.Cost = 0;
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
            finally
            {
                if(_serviceRequestPresentationModel != null)
                {
                    TextBoxCost.Text = $"${_serviceRequestPresentationModel.Cost} MXN";
                } else
                {
                    TextBoxCost.Text = "$0.00 MXN";
                }                
            }
        }

        private void CityComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _serviceRequestPresentationModel.City = (CityPresentationModel)ComboBoxCity.SelectedItem;
            LoadActivePriceRate();
        }

        private void KindOfServiceComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _serviceRequestPresentationModel.KindOfService = int.Parse(((ComboBoxItem)ComboBoxKindOfService.SelectedItem).Tag.ToString());
            if (_serviceRequestPresentationModel.City != null)
            {
                LoadActivePriceRate();
            }
        }

        private void AddressComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _serviceRequestPresentationModel.Address = (AddressPresentationModel)ComboBoxAddress.SelectedItem;
        }
    }
}
