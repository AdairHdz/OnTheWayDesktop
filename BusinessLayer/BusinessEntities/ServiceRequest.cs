using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using Utils;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceRequest
    {
        public string ID { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public Address DeliveryAddress { get; set; }
        public string Description { get; set; }
        public KindOfService KindOfService { get; set; }
        public ServiceStatus ServiceStatus { get; set; }                        
        public ServiceRequester ServiceRequester { get; set; }
        public ServiceProvider ServiceProvider { get; set; }

        public void Save()
        {
            ServiceRequestDTO serviceRequestDTO = ServiceRequestMapper.CreateServiceRequestDTOFromServiceRequestEntity(this);
            RestRequest<object> request = new RestRequest<object>();
            request.Post("requests", serviceRequestDTO);
        }

        public List<ServiceRequest> FindByDate(Dictionary<string, string> queryParameters)
        {
            RestRequest<ServiceRequestResponseDTO> request = new RestRequest<ServiceRequestResponseDTO>();
            List<ServiceRequestResponseDTO> serviceRequests = request.GetAll($"requesters/{Session.GetSession().ID}/requests", true, queryParameters);
            return ServiceRequestMapper.CreateServiceRequestEntitiesListFromServiceRequestDTOList(serviceRequests);            
        }
    }
}
