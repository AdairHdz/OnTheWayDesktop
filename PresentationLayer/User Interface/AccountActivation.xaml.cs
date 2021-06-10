using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.Helpers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para AccountActivation.xaml
    /// </summary>
    public partial class AccountActivation : Window
    {
        private AccountVerificationCodePresentationModel _accountVerificationCodePresentationModel = new AccountVerificationCodePresentationModel();
        public AccountActivation()
        {
            InitializeComponent();
            DataContext = _accountVerificationCodePresentationModel;
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
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

        private void VerifyAccountButtonClicked(object sender, RoutedEventArgs e)
        {
            AccountVerificationCodeValidator accountVerificationCodeValidator = new AccountVerificationCodeValidator();
            ValidationResult validationResult = accountVerificationCodeValidator.Validate(_accountVerificationCodePresentationModel);             
            ShowFeedback(validationResult);
            if (validationResult.IsValid)
            {
                User user = new User();
                user.VerificationCode = _accountVerificationCodePresentationModel.VerificationCode;
                try
                {
                    bool accountWasVerified = user.VerifyAccount();
                    if(accountWasVerified)
                    {
                        ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
                        serviceRequesterMenu.Show();
                        Close();
                    }
                    else
                    {
                        NotificationWindow.ShowErrorWindow("Error", "Ocurrió un error al intentar verificar la cuenta. Por favor, intente más tarde.");
                    }
                    
                }
                catch(NetworkRequestException networkRequestException)
                {
                    string exceptionMessage;
                    switch(networkRequestException.StatusCode)
                    {
                        case 400:
                            exceptionMessage = "Ocurrió al procesar su solicitud debido a que no está formada correctamente.";
                            break;
                        case 409:
                            exceptionMessage = "El código que introdujo no coincide con el código registrado en nuestra base de datos.";
                            break;
                        case 500:
                            exceptionMessage = "Ocurrió un error interno en el servidot. Por favor, intente más tarde.";
                            break;
                        default:
                            exceptionMessage = "Ocurrió un error desconocido. Por favor, intente más tarde.";
                            break;
                    }
                    NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                }
            }
            
        }

        private void RefreshCodeButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User();
                bool codeWasRefreshed = user.RefreshVerificationCode();
                if(codeWasRefreshed)
                {
                    NotificationWindow.ShowNotificationWindow("Nuevo código asignado", "Se ha asignado un nuevo código de verificación y este ha sido enviado a su correo electrónico.");
                }
                else
                {
                    NotificationWindow.ShowErrorWindow("Error al crear nuevo código", "Ha ocurrido un error al intentar " +
                        "generar un nuevo código de verificación. Por favor, intente más tarde.");
                }
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Ocurrió un error al enviar el código a su dirección de correo electrónico.";
                        break;
                    case 409:
                        exceptionMessage = "Ocurrió un error al intentar asignarle un nuevo código de activación. Por favor, intente más tarde.";
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
