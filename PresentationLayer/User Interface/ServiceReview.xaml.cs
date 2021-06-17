using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using Microsoft.Win32;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using PresentationLayer.ValidationModules;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Utils;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceReview.xaml
    /// </summary>
    public partial class ServiceReview : Window
    {
        private ReviewPresentationModel _review = new ReviewPresentationModel
        {
            Evidence = new List<string>()
        };
        public ServiceReview(string serviceProviderID)
        {
            InitializeComponent();            
            DataContext = _review;
            _review.ServiceProviderID = serviceProviderID;
            _review.ServiceRequesterID = Session.GetSession().ID;
        }

        private void BasicRatingBarValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _review.Score = BasicRatingBarServiceScore.Value;            
        }

        private void ReviewButtonClicked(object sender, RoutedEventArgs e)
        {
            ReviewValidator reviewValidator = new ReviewValidator();
            FluentValidation.Results.ValidationResult validationResult = reviewValidator.Validate(_review);
            ShowFeedback(validationResult);         
            if(validationResult.IsValid)
            {
                SaveReview();                         
            }

        }

        private void GoBackToLoginView()
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private async Task SaveReviewEvidenceAsync()
        {
            try
            {
                Review review = ReviewMapper.CreateReviewEntityFromReviewPresentationModel(_review);
                await review.SaveReviewFilesAsync();
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "La evidencia no pudo ser guardada debido a que poseía uno o más archivos con formato no válido.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar la evidencia que proporcionó. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido al intentar guardar la evidencia. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                }                
            }
        }

        private void SaveReview()
        {
            Review review = ReviewMapper.CreateReviewEntityFromReviewPresentationModel(_review);
            try
            {
                review.Save();
                _ = review.SaveReviewFilesAsync();
                NotificationWindow.ShowNotificationWindow("Operación exitosa", "Su reseña se ha publicado correctamente.");
                Close();
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "La petición tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
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

        private void ShowFeedback(FluentValidation.Results.ValidationResult validationResult)
        {
            IList<ValidationFailure> validationFailures = validationResult.Errors;
            UserFeedback userFeedback = new UserFeedback(FormGrid, validationFailures);
            userFeedback.ShowFeedback();
        }

        private void AddFilesButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Multimedia (*.PNG;*.JPG;*.JPEG;MP4)|*.PNG;*.JPG;*.JPEG;*.mp4";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                if(openFileDialog.FileNames.Length > 3)
                {
                    NotificationWindow.ShowErrorWindow("Error", "Solo puede seleccionar un máximo de 3 archivos.");
                }
                else
                {
                    Stream[] selectedFiles = openFileDialog.OpenFiles();
                    for(int index = 0; index < selectedFiles.Length; index++)
                    {
                        if(selectedFiles[index].Length > 9961472)
                        {
                            NotificationWindow.ShowErrorWindow("Archivo demasiado pesado", "Por favor, seleccione archivos que pesen menos de 9MB");
                            return;
                        }
                    }                    
                    _review.Evidence.AddRange(openFileDialog.FileNames);

                    _review.Evidence.ForEach(file =>
                    {
                        TextBlock fileName = new TextBlock
                        {
                            Text = file
                        };
                        StackPanelFileNames.Children.Add(fileName);
                    });                    
                }                                                
            }            
        }
    }
}
