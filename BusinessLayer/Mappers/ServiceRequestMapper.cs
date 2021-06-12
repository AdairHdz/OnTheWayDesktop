using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System;
using System.Collections.Generic;

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

        public static List<ServiceRequest> CreateServiceRequestEntitiesListFromServiceRequestDTOList(List<ServiceRequestResponseDTO> serviceRequestDTOList)
        {
            List<ServiceRequest> listOfServiceRequestEntities = new List<ServiceRequest>();
            serviceRequestDTOList.ForEach(serviceRequestItem =>
            {
                ServiceRequest serviceRequest = new ServiceRequest()
                {                    
                    ID = serviceRequestItem.ID,
                    Cost = serviceRequestItem.Cost,
                    Date = DateTime.Parse(serviceRequestItem.Date),
                    DeliveryAddress = AddressMapper.CreateAddressEntityFromAddressDetailsDTO(serviceRequestItem.DeliveryAddress),
                    Description = serviceRequestItem.Description,
                    KindOfService = (KindOfService)serviceRequestItem.KindOfService,
                    ServiceStatus = (ServiceStatus)serviceRequestItem.Status,
                    ServiceRequester = ServiceRequesterMapper.CreateServiceRequesterFromUserOverviewDTO(serviceRequestItem.ServiceRequester),
                    ServiceProvider = ServiceProviderMapper.CreateServiceProviderFromUserOverviewDTO(serviceRequestItem.ServiceProvider),
                };
                listOfServiceRequestEntities.Add(serviceRequest);
            });
            return listOfServiceRequestEntities;
        }

        public static ServiceRequest CreateServiceRequestEntityFromServiceRequestDTO(ServiceRequestResponseDTO serviceRequestDTO)
        {            
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                ID = serviceRequestDTO.ID,
                Cost = serviceRequestDTO.Cost,
                Date = DateTime.Parse(serviceRequestDTO.Date),
                DeliveryAddress = AddressMapper.CreateAddressEntityFromAddressDetailsDTO(serviceRequestDTO.DeliveryAddress),
                Description = serviceRequestDTO.Description,
                KindOfService = (KindOfService)serviceRequestDTO.KindOfService,
                ServiceStatus = (ServiceStatus)serviceRequestDTO.Status,
                ServiceRequester = ServiceRequesterMapper.CreateServiceRequesterFromUserOverviewDTO(serviceRequestDTO.ServiceRequester),
                ServiceProvider = ServiceProviderMapper.CreateServiceProviderFromUserOverviewDTO(serviceRequestDTO.ServiceProvider),
            };
            return serviceRequest;
        }
    }
}
