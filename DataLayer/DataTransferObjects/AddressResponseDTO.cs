namespace DataLayer.DataTransferObjects
{
    public class AddressResponseDTO
    {
        public string ID { get; set; }
        public string IndoorNumber { get; set; }
        public string OutdoorNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public CityDTO City { get; set; }
    }
}
