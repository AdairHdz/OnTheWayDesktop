namespace PresentationLayer.PresentationModels
{
    public class ServiceRequestHistoryPresentationModel
    {
        public string ID { get; set; }
        public string KindOfService { get; set; }
        public double Cost { get; set; }
        public string Status { get; set; }
        public string ServiceProvider { get; set; }
    }
}
