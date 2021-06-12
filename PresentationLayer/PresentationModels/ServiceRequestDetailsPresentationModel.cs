namespace PresentationLayer.PresentationModels
{
    public class ServiceRequestDetailsPresentationModel
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string DeliveryAddress { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double Cost { get; set; }
        public string ServiceProviderID { get; set; }
    }
}
