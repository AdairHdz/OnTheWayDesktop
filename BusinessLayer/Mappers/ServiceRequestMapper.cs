using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;

namespace BusinessLayer.Mappers
{
    public class ServiceRequestMapper
    {
        public static ServiceRequestDTO CreateServiceRequestDTOFromServiceRequestEntity(ServiceRequest serviceRequest)
        {
            ServiceRequestDTO serviceRequestDTO = new ServiceRequestDTO
            {
                Cost = serviceRequest.Cost,
                DeliveryAddressID = serviceRequest.DeliveryAddress.ID,
                Description = serviceRequest.Description,
                KindOfService = (int)serviceRequest.KindOfService,
                ServiceProviderID = serviceRequest.ServiceProvider.ID,
                ServiceRequesterID = serviceRequest.ServiceRequester.ID
            };
            return serviceRequestDTO;
        }
    }
}
