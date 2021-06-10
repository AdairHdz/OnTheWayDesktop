using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class PriceRateDTO
    {
        public string ID { get; set; }
        public string StartingHour { get; set; }
        public string EndingHour { get; set; }
        public double Price { get; set; }
        public int KindOfService { get; set; }
        public CityDTO City { get; set; }
        public List<int> WorkingDays { get; set; }

    }
}
