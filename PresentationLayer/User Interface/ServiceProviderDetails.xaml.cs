using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using Flurl;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceProviderDetails.xaml
    /// </summary>
    public partial class ServiceProviderDetails : Window
    {
        private string _serviceProviderID;
        private ServiceProviderDetailPresentationModel _serviceProviderDetails = new ServiceProviderDetailPresentationModel();
        private ReviewPaginationDTO _reviews;
        private int _page = 1;
        private int _pagesize;
        public ServiceProviderDetails(string serviceProviderID)
        {
            _serviceProviderID = serviceProviderID;
            _pagesize = 5;
            InitializeComponent();
        }

        private void LoadServiceProviderImage()
        {            
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var root = Directory.GetCurrentDirectory();
            string path = System.IO.Path.Combine(root, @"..\..\Images\OnTheWay.png");            
                        
            BitmapImage image = new BitmapImage();
            image.BeginInit();            
            image.UriSource = new Uri(Url.Combine("http://localhost:8080/images", $"{_serviceProviderID}/{_serviceProviderDetails.ProfileImage}"));            
            image.EndInit();
            ServiceProviderImage.Source = image; 
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersSearch serviceProvidersSearch = new ServiceProvidersSearch();
            serviceProvidersSearch.Show();
            Close();
        }

        private void RequestServiceButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequest serviceRequest = new ServiceRequest(_serviceProviderID);
            serviceRequest.Show();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadServiceProviderData();
            LoadServiceProviderImage();
            PrintPriceRates();
            LoadReviews();
        }

        private void NavigateToServiceProvidersSearch()
        {
            ServiceProvidersSearch serviceProvidersSearch = new ServiceProvidersSearch();
            serviceProvidersSearch.Show();
            Close();
        }

        private void GoBackToLoginView()
        {
            Login login = new Login();
            login.Show();
            Close();
        }
        private void LoadServiceProviderData()
        {
            try
            {
                ServiceProvider serviceProvider = new ServiceProvider();
                _serviceProviderDetails =
                    ServiceProviderMapper.CreateServiceProviderPresentationModelFromEntity(serviceProvider.Find(_serviceProviderID));
                DataContext = _serviceProviderDetails;
                BasicRatingBar.Value = _serviceProviderDetails.AverageScore;
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        NavigateToServiceProvidersSearch();
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);                        
                        GoBackToLoginView();
                        break;
                    case 404:
                        exceptionMessage = "No se encontraron coincidencias para la información solicitada. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        NavigateToServiceProvidersSearch();
                        break;
                    case 409:
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        NavigateToServiceProvidersSearch();
                        break;
                    case 500:
                        exceptionMessage = "Ha ocurrido un error interno en el servidor. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        NavigateToServiceProvidersSearch();
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        NavigateToServiceProvidersSearch();
                        break;
                }                
            }
        }

        private void PrintPriceRates()
        {
            _serviceProviderDetails.PriceRates.ForEach(priceRateElement =>
            {
                StackPanel priceRateContainer = new StackPanel();

                TextBlock shcedule = new TextBlock
                {
                    Text = $"Horario: {priceRateElement.StartingHour} - {priceRateElement.EndingHour}",
                    Margin = new Thickness(0, 0, 0, 15),
                    FontSize = 18,
                    FontWeight = FontWeights.Bold
                };                

                TextBlock price = new TextBlock
                {
                    Text = $"${priceRateElement.Price} MXN",
                    Margin = new Thickness(0, 0, 0, 15),
                    FontSize = 18,
                    FontWeight = FontWeights.Bold
                };

                TextBlock kindOfService = new TextBlock
                {
                    Text = priceRateElement.KindOfService
                };

                TextBlock cityPriceRate = new TextBlock
                {
                    Text = priceRateElement.City,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                };

                DockPanel workingDaysContainer = new DockPanel()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(0, 0, 0, 10),
                };

                priceRateElement.WorkingDays.ForEach(workingDay =>
                {
                    StackPanel workingDaySymbol = new StackPanel
                    {
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(0, 0, 5, 0),
                        ToolTip = workingDay
                    };

                    Ellipse workingDayEllipse = new Ellipse
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 30,
                        Height = 30,
                        Fill = new SolidColorBrush(Color.FromRgb(22, 110, 229)),
                    };

                    TextBlock workingDayText = new TextBlock
                    {
                        Text = workingDay.Substring(0, 1),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Colors.White),
                        Margin = new Thickness(0, -25, 0, 0)
                    };

                    workingDaySymbol.Children.Add(workingDayEllipse);
                    workingDaySymbol.Children.Add(workingDayText);
                    workingDaysContainer.Children.Add(workingDaySymbol);
                });

                priceRateContainer.Children.Add(workingDaysContainer);
                priceRateContainer.Children.Add(cityPriceRate);
                priceRateContainer.Children.Add(shcedule);
                priceRateContainer.Children.Add(price);
                priceRateContainer.Children.Add(kindOfService);
                StackPanelPriceRates.Children.Add(priceRateContainer);


                Separator separator = new Separator
                {
                    Height = 10
                };
                priceRateContainer.Children.Add(separator);
            });

        }

        private void LoadReviews()
        {
            ReviewsStackPanel.Children.Clear();
            try
            {
                Review review = new Review();
                Dictionary<string, string> queryParameters = new Dictionary<string, string>
                {
                    ["page"] = _page.ToString(),
                    ["pagesize"] = _pagesize.ToString()
                };
                _reviews = review.FindAll(_serviceProviderID, queryParameters);
                if (_reviews.Page > 1)
                {
                    StartingPageButton.IsEnabled = true;
                    PreviousPageButton.IsEnabled = true;
                }
                else
                {
                    StartingPageButton.IsEnabled = false;
                    PreviousPageButton.IsEnabled = false;
                }


                if (_reviews.Page < _reviews.Pages)
                {
                    LastPageButton.IsEnabled = true;
                    NextPageButton.IsEnabled = true;
                }
                else
                {
                    LastPageButton.IsEnabled = false;
                    NextPageButton.IsEnabled = false;
                }
                PrintReviews();
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch(networkRequestException.StatusCode)
                {
                    case 400:                    
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    case 401:
                        exceptionMessage = "Lo sentimos, su sesión ha expirado";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        GoBackToLoginView();
                        break;
                    case 404:
                        ReviewsStackPanel.Children.Clear();
                        TextBlock noReviewsNotification = new TextBlock
                        {
                            Text = "Este proveedor de servicio aún no tiene reseñas",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        };

                        ReviewsStackPanel.Children.Add(noReviewsNotification);
                        break;
                    case 409:
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    case 500:
                        exceptionMessage = "Ha ocurrido un error interno en el servidor. Por favor, intente más tarde.";
                        NotifyErrorAndDisableButtons(exceptionMessage);
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
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

        private void PrintReviews()
        {
            ReviewsStackPanel.Children.Clear();
            CurrentPageButton.Content = _reviews.Page;
            _reviews.Data.ForEach(reviewElement =>
            {
                StackPanel stackPanel = new StackPanel
                {
                    Margin = new Thickness(0, 0, 0, 30)
                };

                TextBlock reviewTitle = new TextBlock
                {
                    Text = reviewElement.Title,
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 10)
                };

                MaterialDesignThemes.Wpf.RatingBar ratingBar = new MaterialDesignThemes.Wpf.RatingBar()
                {
                    Value = reviewElement.Score,
                    Foreground = new SolidColorBrush(Colors.Gold),
                    Margin = new Thickness(0, 0, 0, 10)
                };

                TextBlock reviewDetails = new TextBlock
                {
                    Text = reviewElement.Details,
                };

                TextBlock serviceRequesterName = new TextBlock
                {
                    Text = reviewElement.ServiceRequester.Names + " " + reviewElement.ServiceRequester.LastName,
                    FontSize = 12,
                    Foreground = new SolidColorBrush(Colors.DarkGray)
                };

                stackPanel.Children.Add(reviewTitle);
                stackPanel.Children.Add(serviceRequesterName);
                stackPanel.Children.Add(ratingBar);
                stackPanel.Children.Add(reviewDetails);

                DockPanel reviewEvidence = new DockPanel
                {
                    Margin = new Thickness(0, 30, 0, 30)
                };

                if(reviewElement.Evidence != null)
                {
                    reviewElement.Evidence.ForEach(evidenceElement =>
                    {
                        TextBlock buttonTextBlock = new TextBlock()
                        {
                            Text = evidenceElement.Name,
                            TextAlignment = TextAlignment.Center,
                            TextWrapping = TextWrapping.Wrap
                        };
                        Button evidenceButton = new Button
                        {
                            Content = buttonTextBlock,
                            Foreground = new SolidColorBrush(Colors.White),
                            MaxWidth = 180,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(0, 0, 5, 0),
                            Tag = evidenceElement.Link

                        };
                        evidenceButton.Click += NavigateToEvidenceWindow;
                        reviewEvidence.Children.Add(evidenceButton);
                    });
                    stackPanel.Children.Add(reviewEvidence);                   
                }
                ReviewsStackPanel.Children.Add(stackPanel);
            });
        }

        void NavigateToEvidenceWindow(object sender, RoutedEventArgs e)
        {
            string linkOfEvidence = ((Button)sender).Tag.ToString();
            EvidenceViewer evidenceViewer = new EvidenceViewer(linkOfEvidence);
            evidenceViewer.Show();
        }

        private void FirstPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page = 1;
            LoadReviews();

        }

        private void PreviousPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page--;
            LoadReviews();

        }

        private void NextPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page++;
            LoadReviews();

        }

        private void LastPageButtonClicked(object sender, RoutedEventArgs e)
        {
            _page = _reviews.Pages;
            LoadReviews();
        }

        private void PageSizeComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            _pagesize = int.Parse(((ComboBoxItem)ComboBoxPageSize.SelectedItem).Tag.ToString());
            LoadReviews();
        }
    }    
}