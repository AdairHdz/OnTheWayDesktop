using BusinessLayer.BusinessEntities;
using PresentationLayer.Helpers;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceRequest.xaml
    /// </summary>
    public partial class ServiceRequest : Window
    {
        public ServiceRequest()
        {
            InitializeComponent();
            Address address = new Address();
            try
            {
                List<Address> listOfAddresses = address.GetAddresses();
                listOfAddresses.ForEach(item =>
                {
                    NotificationWindow.ShowNotificationWindow("Dirección", item.Street);
                });
            }
            catch (NetworkRequestException networkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
            }
            
        }
    }
}
