namespace BusinessLayer.BusinessEntities
{
    public class Address
    {
        public string ID { get; set; }
        public int IndoorNumber { get; set; }
        public int OutdoorNumber { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public City City { get; set; }
    }
}
