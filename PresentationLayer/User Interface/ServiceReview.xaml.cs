using BusinessLayer.BusinessEntities;
using FluentValidation.Results;
using PresentationLayer.ValidationModules;
using System;
using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceReview.xaml
    /// </summary>
    public partial class ServiceReview : Window
    {
        private Review _review = new Review();
        public ServiceReview(BusinessLayer.BusinessEntities.ServiceRequest serviceRequest)
        {
            InitializeComponent();
            _review.ServiceProvider = serviceRequest.ServiceProvider;
            this.DataContext = _review;
        }

        private void BasicRatingBarValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            _review.Score = BasicRatingBarServiceScore.Value;
            ReviewValidator reviewValidator = new ReviewValidator();
            ValidationResult validationResult = reviewValidator.Validate(_review);
            Console.WriteLine(validationResult.IsValid);
        }
    }
}
