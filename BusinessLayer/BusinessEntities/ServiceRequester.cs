using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;
using Utils;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceRequester : User 
    {
        public string ID { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
        public List<Address> Addresses { get; set; }


        public Statistics GetStatistics(Dictionary<string, string> queryParameters)
        {
            RestRequest<StatisticsResponseDTO> request = new RestRequest<StatisticsResponseDTO>();
            StatisticsResponseDTO response = request.Get($"requesters/{Session.GetSession().ID}/statistics", true, queryParameters);
            return StatisticsMapper.CreateStatisticsEntityFromStatisticsDTO(response);
        }

    }


}
