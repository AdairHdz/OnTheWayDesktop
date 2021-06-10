﻿using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class PriceRate
    {
        public string ID { get; set; }
        public string StartingHour { get; set; }
        public string EndingHour { get; set; }
        public double Price { get; set; }
        public List<DayOfWeek> WorkingDays { get; set; }
        public City City { get; set; }
        public KindOfService KindOfService { get; set; }
    }
}
