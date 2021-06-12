using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class StatisticsMapper
    {
        public static Statistics CreateStatisticsEntityFromStatisticsDTO(StatisticsResponseDTO statisticsResponseDTO)
        {
            Statistics statistics = new Statistics
            {
                RequestedServicesPerWeekday = CreateListOfRequestedServicesPerWeekday(statisticsResponseDTO.RequestedServicesPerWeekday),
                RequestedServicesPerKindOfService = CreateListOfRequestedServicesPerKindOfService(statisticsResponseDTO.RequestedServicesPerKindOfService)

            };
            return statistics;
        }

        private static List<RequestedServicesPerKindOfService> CreateListOfRequestedServicesPerKindOfService(List<RequestedServicesPerKindOfServiceDTO> requestedServicesPerKindOfServiceDTO)
        {
            List<int>kindsOfService = new List<int>{ 0, 1, 2, 3, 4};
            List<RequestedServicesPerKindOfService> requestedServicesPerKindOfService = new List<RequestedServicesPerKindOfService>();
            requestedServicesPerKindOfServiceDTO.ForEach(requestedService =>
            {
                kindsOfService.Remove(requestedService.KindOfService);
                requestedServicesPerKindOfService.Add(new RequestedServicesPerKindOfService
                {
                    KindOfService = requestedService.KindOfService,
                    RequestedServices = requestedService.RequestedServices
                });
            });

            kindsOfService.ForEach(element =>
            {
                requestedServicesPerKindOfService.Add(new RequestedServicesPerKindOfService
                {
                    KindOfService = element,
                    RequestedServices = 0
                });
            });
            return requestedServicesPerKindOfService;
        }

        private static List<RequestedServicesPerWeekday> CreateListOfRequestedServicesPerWeekday(List<RequestedServicesPerWeekdayDTO> requestedServicesPerWeekdayDTOs)
        {
            List<RequestedServicesPerWeekday> requestedServicesPerWeekdays = new List<RequestedServicesPerWeekday>();
            List<int> weekdays = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            requestedServicesPerWeekdayDTOs.ForEach(requestedService =>
            {
 
                weekdays.Remove(requestedService.Weekday);
;
                requestedServicesPerWeekdays.Add(new RequestedServicesPerWeekday
                {
                    Weekday = requestedService.Weekday,
                    RequestedServices = requestedService.RequestedServices
                });
            });

            weekdays.ForEach(element =>
            {
                requestedServicesPerWeekdays.Add(new RequestedServicesPerWeekday
                {
                    Weekday = element,
                    RequestedServices = 0
                });
            });
            return requestedServicesPerWeekdays;
        }
    }
}
