using System.Collections.Generic;

namespace PresentationLayer.PresentationModels
{
    public class StatisticsPresentationModel
    {
        public List<RequestedServicesPerWeekday> RequestedServicesPerWeekday { get; set; }
        public List<RequestedServicesPerKindOfService> RequestedServicesPerKindOfService { get; set; }
    }

    public class RequestedServicesPerWeekday
    {
        public int RequestedServices { get; set; }
        public int Weekday { get; set; }
    }

    public class RequestedServicesPerKindOfService
    {
        public int RequestedServices { get; set; }
        public int KindOfService { get; set; }
    }
}
