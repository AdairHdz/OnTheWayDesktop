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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginPresentationModel _loginPresentationModel;
        public Login()
        {
            InitializeComponent();
            _loginPresentationModel = new LoginPresentationModel();
            this.DataContext = _loginPresentationModel;
        }

        private void CreateAccountButtonClicked(object sender, RoutedEventArgs e)
        {
            Registry registry = new Registry();
            registry.Show();
            Close();
        }        

        private void ShowFeedback(ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(FormGrid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {            
            _loginPresentationModel.Password = PasswordBoxPassword.Password;
            LoginPresentationModelValidator loginPresentationModelValidator = new LoginPresentationModelValidator();
            ValidationResult validationResult = loginPresentationModelValidator.Validate(_loginPresentationModel);
            ShowFeedback(validationResult);
            if (validationResult.IsValid)
            {
                try
                {
                    LogUserIn();
                }
                catch (NetworkRequestException networkRequestException)
                {
                    NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                }
            }

        }

        private void LogUserIn()
        {
            User user = UserMapper.CreateUserEntityFromLogin(_loginPresentationModel);
            bool userCouldLogIn = user.Login();

            if (userCouldLogIn)
            {
                Session session = Session.GetSession();
                if (session.Verified)
                {
                    ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
                    serviceRequesterMenu.Show();
                    Close();
                }
                else
                {
                    AccountActivation accountActivation = new AccountActivation();
                    accountActivation.Show();
                    Close();
                }
            }
            else
            {
                NotificationWindow.ShowErrorWindow("Credenciales no válidas", "La dirección de correo electrónico" +
                    " o la contraseña que introdujo no coinciden con nuestros" +
                    "registros en la base de datos. Por favor, verifique la información e intente de nuevo.");
            }        
        }

        private void ForgotPasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordRecovery = new PasswordRecovery();
            passwordRecovery.Show();
            Close();
        }
    }
}
