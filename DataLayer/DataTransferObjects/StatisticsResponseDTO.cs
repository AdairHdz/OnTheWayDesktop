using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class StatisticsResponseDTO
    {
        public List<RequestedServicesPerWeekdayDTO> RequestedServicesPerWeekday { get; set; }
        public List<RequestedServicesPerKindOfServiceDTO> RequestedServicesPerKindOfService { get; set; }
    }

    public class RequestedServicesPerWeekdayDTO
    {
        public int RequestedServices { get; set; }
        public int Weekday { get; set; }
    }

    public class RequestedServicesPerKindOfServiceDTO
    {
        public int RequestedServices { get; set; }
        public int KindOfService { get; set; }
    }
}
