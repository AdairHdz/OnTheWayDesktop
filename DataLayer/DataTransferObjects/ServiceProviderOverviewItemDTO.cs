using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class ServiceProviderOverviewItemDTO
    {
        public string ID { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public int AverageScore { get; set; }
        public double PriceRate { get; set; }
    }
}
