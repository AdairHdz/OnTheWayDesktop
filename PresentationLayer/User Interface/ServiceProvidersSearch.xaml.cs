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
            SeeDetailsButton.IsEnabled = true;
        }

        private void MaxPriceSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double maxPriceSliderValue = MaxPriceSlider.Value;          
            MaxPriceText.Text = $"{maxPriceSliderValue:N2} MXN";
        }

        private void ServiceProvidersListViewSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(ServiceProvidersListView.SelectedItem != null)
            {
                SeeDetailsButton.IsEnabled = true;
            }
        }

        private void SeeDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequest serviceRequest = new ServiceRequest();
            serviceRequest.Show();
        }
    }
}
