using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceProvidersSearch.xaml
    /// </summary>
    public partial class ServiceProvidersSearch : Window
    {
        public ServiceProvidersSearch()
        {
            InitializeComponent();
        }

        private void MaxPriceSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double maxPriceSliderValue = MaxPriceSlider.Value;          
            MaxPriceText.Text = $"{maxPriceSliderValue:N2} MXN";
        }
    }
}
