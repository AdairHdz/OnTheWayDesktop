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
        public ServiceReview()
        {
            InitializeComponent();
            this.DataContext = _review;
        }

        private void BasicRatingBarValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            ReviewValidator reviewValidator = new ReviewValidator();
            ValidationResult validationResult = reviewValidator.Validate(_review);
            Console.WriteLine(validationResult.IsValid);

        }
    }
}
