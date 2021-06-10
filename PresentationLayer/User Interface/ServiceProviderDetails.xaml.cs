﻿using BusinessLayer.BusinessEntities;
using PresentationLayer.Helpers;
using PresentationLayer.Mappers;
using PresentationLayer.PresentationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        private List<Review> _reviews;
        public ServiceProviderDetails(string serviceProviderID)
        {
            _serviceProviderID = serviceProviderID;
            InitializeComponent();
        }

        private void LoadServiceProviderImage()
        {            
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var root = Directory.GetCurrentDirectory();
            string path = System.IO.Path.Combine(root, @"..\..\Images\OnTheWay.png");            
                        
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("http://192.168.100.41:8080/images/82e49b2e-cff5-46cb-8f4f-de932cbb6cbf/OnTheWayIcon.png");
            if(image.SourceRect.IsEmpty)
            {
                image.UriSource = new Uri(path);
            }
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
            ServiceRequest serviceRequest = new ServiceRequest();
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
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                NavigateToServiceProvidersSearch();
            }
            catch (EmptyCollectionException)
            {
                NotificationWindow.ShowErrorWindow("Error", "No se encontró al usuario solicitado");
                NavigateToServiceProvidersSearch();
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
                    Text = priceRateElement.Price.ToString()
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
            try
            {
                Review review = new Review();
                Dictionary<string, string> queryParameters = new Dictionary<string, string>();
                queryParameters["page"] = "1";
                queryParameters["pagesize"] = "5";
                _reviews = review.FindAll(_serviceProviderID, queryParameters);
                PrintReviews();
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch(networkRequestException.StatusCode)
                {
                    case 400:                    
                        exceptionMessage = "Ha ocurrido un error al intentar procesar su solicitud. Por favor, intente más tarde.";
                        NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
                        break;
                    case 404:
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

        private void PrintReviews()
        {
            _reviews.ForEach(reviewElement =>
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
                    Text = reviewElement.ServiceRequester.Names + " " + reviewElement.ServiceRequester.Lastname,
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

                reviewElement.Evidence.ForEach(evidenceElement =>
                {
                    TextBlock buttonTextBlock = new TextBlock()
                    {
                        Text = evidenceElement,
                        TextAlignment = TextAlignment.Center,
                        TextWrapping = TextWrapping.Wrap
                    };
                    Button evidenceButton = new Button
                    {
                        Content = buttonTextBlock,
                        Foreground = new SolidColorBrush(Colors.White),
                        MaxWidth = 100,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(0, 0, 5, 0),
                        Tag = evidenceElement

                    };
                    evidenceButton.Click += NavigateToEvidenceWindow;
                    reviewEvidence.Children.Add(evidenceButton);
                });

                stackPanel.Children.Add(reviewEvidence);

                ReviewsStackPanel.Children.Add(stackPanel);
            });
        }

        void NavigateToEvidenceWindow(object sender, RoutedEventArgs e)
        {
            string idOfEvidence = ((Button)sender).Tag.ToString();
            NotificationWindow.ShowNotificationWindow("ID", idOfEvidence);
            //EvidenceViewer evidenceViewer = new EvidenceViewer();
            //evidenceViewer.Show();
        }
    }
}