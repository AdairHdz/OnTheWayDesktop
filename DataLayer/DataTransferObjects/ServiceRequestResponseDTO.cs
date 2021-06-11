namespace DataLayer.DataTransferObjects
{
    public class ServiceRequestResponseDTO
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public double Cost { get; set; }
        public AddressDetailsDTO DeliveryAddress { get; set; }
        public string Description { get; set; }        
        public int KindOfService { get; set; }
        public UserOverviewDTO ServiceProvider { get; set; }
        public UserOverviewDTO ServiceRequester { get; set; }
    }
}
