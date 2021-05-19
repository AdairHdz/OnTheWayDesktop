using BusinessLayer.BusinessEntities;
using System.Windows;
using System.Windows.Controls;

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
            ServiceProviderDetails serviceProviderDetails = new ServiceProviderDetails();
            serviceProviderDetails.Show();
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceProvidersListView.Items.Clear();
            double maxPriceRate = MaxPriceSlider.Value;
            string cityName = ComboBoxCity.Text;
            var kindOfService = ((ComboBoxItem)ComboBoxKindOfService.SelectedItem).Tag;

            ServiceProvider serviceProvider = new ServiceProvider();
            serviceProvider.FindMatches(maxPriceRate, cityName, kindOfService);
            
            
        }
    }
}
