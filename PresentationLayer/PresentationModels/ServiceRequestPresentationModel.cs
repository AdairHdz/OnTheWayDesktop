namespace PresentationLayer.PresentationModels
{
    public class ServiceRequestPresentationModel
    {
        public int KindOfService { get; set; }
        public AddressPresentationModel Address { get; set; }
        public CityPresentationModel City { get; set; }
        public string AdditionalDetails { get; set; }
        public double Cost { get; set; }
        public string ServiceRequesterID { get; set; }
        public string ServiceProviderID { get; set; }
    }
}
