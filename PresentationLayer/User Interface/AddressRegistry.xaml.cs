using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.Windows;
using Utils;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para AddressRegistry.xaml
    /// </summary>
    public partial class AddressRegistry : Window
    {
        private AddressPresentationModel _addressPresentationModel;
        public AddressRegistry()
        {
            InitializeComponent();
            LoadCities();
            _addressPresentationModel = new AddressPresentationModel();
            DataContext = _addressPresentationModel;
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
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
                        break;
                    case 404:
                        exceptionMessage = "Lo sentimos, pero parece que OnTheWay no cuenta con ciudades registradas para su Estado.";
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

        private void NavigateToServiceRequesterMenu()
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }

        private void RegisterButtonClicked(object sender, RoutedEventArgs e)
        {
            CityPresentationModel selectedCity = (CityPresentationModel)ComboBoxCity.SelectedItem;
            _addressPresentationModel.City = selectedCity;

            AddressPresentationModelValidator registryPresentationModelValidator = new AddressPresentationModelValidator();
            ValidationResult validationResult = registryPresentationModelValidator.Validate(_addressPresentationModel);
            ShowFeedback(validationResult);

            if (validationResult.IsValid)
            {
                try
                {
                    RegisterAddress();
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
                            exceptionMessage = "Lo sentimos, su sesión ha expirado.";
                            NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                            GoBackToLoginView();
                            break;
                        case 409:
                            exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
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
        }

        private void GoBackToLoginView()
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void ShowFeedback(ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(FormGrid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void RegisterAddress()
        {
            Address address = AddressMapper.CreateAddressEntity(_addressPresentationModel);
            address.Register();
            NotificationWindow.ShowNotificationWindow("Dirección registrada",
                "La dirección se ha registrado exitosamente.");
            NavigateToServiceRequesterMenu();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigateToServiceRequesterMenu();
        }
    }
}