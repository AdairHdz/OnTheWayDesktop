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
                    NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                }
            }
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
