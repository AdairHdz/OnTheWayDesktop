using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class ServiceProviderDetailDTO
    {
        public string ID { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int AverageScore { get; set; }
        public List<PriceRateDTO> PriceRates { get; set; }
    }
}