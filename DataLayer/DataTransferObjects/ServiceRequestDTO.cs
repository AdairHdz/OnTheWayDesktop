namespace DataLayer.DataTransferObjects
{
    public class ServiceRequestDTO
    {
        public double Cost { get; set; }
        public string DeliveryAddressID { get; set; }
        public string Description { get; set; }
        public int KindOfService { get; set; }
        public string ServiceProviderID { get; set; }
        public string ServiceRequesterID { get; set; }
    }
}
