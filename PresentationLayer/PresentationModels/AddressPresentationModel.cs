namespace PresentationLayer.PresentationModels
{
    public class AddressPresentationModel
    {
        public string ID { get; set; }
        public string IndoorNumber { get; set; }
        public string OutdoorNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public CityPresentationModel City { get; set; }
        public string AddressOverview { get; set; }
    }
}