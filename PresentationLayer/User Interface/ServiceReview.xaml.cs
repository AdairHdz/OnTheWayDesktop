using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceReview.xaml
    /// </summary>
    public partial class ServiceReview : Window
    {
        public ServiceReview()
        {
            InitializeComponent();
        }

        private void BasicRatingBarValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {            
        }
    }
}
