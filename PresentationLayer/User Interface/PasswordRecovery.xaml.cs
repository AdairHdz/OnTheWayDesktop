using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para PasswordRecovery.xaml
    /// </summary>
    public partial class PasswordRecovery : Window
    {
        private PasswordRecoveryCodePresentationModel _recoveryCodePresentationModel = new PasswordRecoveryCodePresentationModel();
        private EmailAddressPresentationModel _emailAddressPresentationModel = new EmailAddressPresentationModel();
        public PasswordRecovery()
        {
            InitializeComponent();
            RestablishPasswordFormGrid.DataContext = _recoveryCodePresentationModel;
            EmailFormGrid.DataContext = _emailAddressPresentationModel;
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void ShowFeedback(Grid grid, FluentValidation.Results.ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(grid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void RestablishPasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            _recoveryCodePresentationModel.Password = PasswordBoxPassword.Password;
            _recoveryCodePresentationModel.Passwordconfirmation = PasswordConfirmationBoxPassword.Password;
            PasswordRecoveryCodeValidator passwordRecoveryCodeValidator = new PasswordRecoveryCodeValidator();
            FluentValidation.Results.ValidationResult validationResult = passwordRecoveryCodeValidator.Validate(_recoveryCodePresentationModel);
            ShowFeedback(RestablishPasswordFormGrid, validationResult);
            if (validationResult.IsValid)
            {
                try
                {
                    RestablishPassword();
                }
                catch(NetworkRequestException networkRequestException)
                {
                    string exceptionMessage;
                    switch(networkRequestException.StatusCode)
                    {
                        case 400:
                            exceptionMessage = "Error. El ID de usuario de su sesión no es válido.";
                            break;
                        case 409:
                            exceptionMessage = "Ocurrió un error al intentar restablecer su contraseña. Por favor, verifique los datos e intente más tarde.";
                            break;
                        case 500:
                            exceptionMessage = "Ocurrió un error interno en el servidor. Por favor, intente más tarde.";
                            break;
                        default:
                            exceptionMessage = "Ocurrió un error inesperado. Por favor, intente más tarde.";
                            break;
                    }
                    NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                }                
            }
        }

        private void RestablishPassword()
        {
            User user = new User
            {
                RecoveryCode = _recoveryCodePresentationModel.RecoveryCode,
                Password = _recoveryCodePresentationModel.Password,
                EmailAddress = _recoveryCodePresentationModel.RecoveryEmail
            };
            bool passwordWasSuccessfullyRestablished = user.RestablishPassword();
            if (passwordWasSuccessfullyRestablished)
            {
                NotificationWindow.ShowNotificationWindow("Contraseña restablecida", "Su contraseña fue restablecida correctamente.");
                NavigateToLoginView();
            }
            else
            {
                NotificationWindow.ShowErrorWindow("Error al restablecer contraseña.", "Ocurrió un error al intentar restablecer su contraseña. Por favor, intente más tarde.");
            }
        }

        private void NavigateToLoginView()
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void ResendCodeButtonClicked(object sender, RoutedEventArgs e)
        {
            EmailAddressValidator emailAddressValidator = new EmailAddressValidator();
            FluentValidation.Results.ValidationResult validationResult = emailAddressValidator.Validate(_emailAddressPresentationModel);
            ShowFeedback(EmailFormGrid, validationResult);
            if (validationResult.IsValid)
            {
                User user = new User
                {
                    EmailAddress = _emailAddressPresentationModel.EmailAddress
                };

                try
                {
                    user.ResendRecoveryCode();
                    NotificationWindow.ShowNotificationWindow("Nuevo código generado", "Se ha enviado un nuevo código de recuperació a su cuenta de correo.");
                }
                catch(NetworkRequestException networkRequestException)
                {
                    string exceptionMessage;
                    switch(networkRequestException.StatusCode)
                    {
                        case 400:
                            exceptionMessage = "El email que introdujo no es válido.";
                            break;
                        case 409:
                            exceptionMessage = "Ocurrió un problema al intentar generar un nuevo código de recuperación.";
                            break;
                        case 500:
                            exceptionMessage = "Ocurrió un error interno en el servidor. Por favor, intente más tarde.";
                            break;
                        default:
                            exceptionMessage = "Ocurrió un error desconocido. Por favor, intente más tarde.";
                            break;
                    }
                    NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                }
                
                
            
            }
        }
    }
}
