using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    public partial class Registry : Window
    {
        private RegistryPresentationModel _registryPresentationModel = new RegistryPresentationModel();
        private List<StatePresentationModel> _states = new List<StatePresentationModel>();
        public Registry()
        {
            InitializeComponent();
            this.DataContext = _registryPresentationModel;
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigateToLoginView();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            StatePresentationModel selectedState = (StatePresentationModel)ComboBoxState.SelectedItem;
            _registryPresentationModel.Password = PasswordBoxPassword.Password;
            _registryPresentationModel.State = selectedState;

            RegistryPresentationModelValidator registryPresentationModelValidator = new RegistryPresentationModelValidator();
            ValidationResult validationResult = registryPresentationModelValidator.Validate(_registryPresentationModel);
            ShowFeedback(validationResult);

            if (validationResult.IsValid)
            {
                try
                {
                    RegisterUser();
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
        }

        private void ShowFeedback(ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(FormGrid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void RegisterUser()
        {
            User user = UserMapper.CreateUserEntityFromRegistry(_registryPresentationModel);
            user.Register();
            NotificationWindow.ShowNotificationWindow("Usuario registrado",
                "Su cuenta se ha registrado exitosamente. Ahora podrá iniciar sesión");
            NavigateToLoginView();

        }


        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStates();
            }catch(NetworkRequestException networkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                NavigateToLoginView();
            }
            
        }

        private void LoadStates()
        {
            State state = new State();
            _states = StateMapper.CreateListOfStatePrecentationModel(state.GetStates());
            _states.ForEach(retrievedState =>
            {
                ComboBoxState.Items.Add(retrievedState);
            });
        }

        private void NavigateToLoginView()
        {
            Login loginView = new Login();
            loginView.Show();
            Close();
        }
    }
}
