using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System;
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
                catch (TimeoutException timeoutException)
                {
                    NotificationWindow.ShowErrorWindow("Error", timeoutException.Message);
                }
                catch (NetworkRequestException networkRequestException)
                {
                    string exceptionMessage;
                    switch(networkRequestException.StatusCode)
                    {
                        case 401:
                            exceptionMessage = "La dirección de correo electrónico" +
                            " o la contraseña que introdujo no coinciden con nuestros" +
                            "registros en la base de datos. Por favor, verifique la información e intente de nuevo.";
                            break;
                        case 409:
                        case 500:
                            exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar la solicitud. Por favor, intente más tarde.";
                            break;
                        default:
                            exceptionMessage = "Ha ocurrido un error desconocido";
                            break;
                    }
                    NotificationWindow.ShowErrorWindow("Error", exceptionMessage);                                     
                }
            }

        }

        private void LogUserIn()
        {
            User user = UserMapper.CreateUserEntityFromLogin(_loginPresentationModel);
            user.Login();
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

        private void ForgotPasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordRecovery = new PasswordRecovery();
            passwordRecovery.Show();
            Close();
        }
    }
}
