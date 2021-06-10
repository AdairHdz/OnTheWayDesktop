using System;
using System.Collections.Generic;

namespace PresentationLayer.PresentationModels
{
    public class PriceRatePresentationModel
    {
        public string StartingHour { get; set; }
        public string EndingHour { get; set; }
        public double Price { get; set; }
        public string KindOfService { get; set; }
        public string City { get; set; }
        public List<string> WorkingDays { get; set; }
    }
}