using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.Mappers
{
    public class ServiceRequestMapper
    {
        public static ServiceRequest CreateServiceRequestEntityFromServiceRequestPresentationModel(ServiceRequestPresentationModel serviceRequestPresentationModel)
        {
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                Cost = serviceRequestPresentationModel.Cost,
                Description = serviceRequestPresentationModel.AdditionalDetails,
                KindOfService = (KindOfService)serviceRequestPresentationModel.KindOfService,
                ServiceProvider = new ServiceProvider()
                {
                    ID = serviceRequestPresentationModel.ServiceProviderID
                },
                ServiceRequester = new ServiceRequester()
                {
                    ID = serviceRequestPresentationModel.ServiceRequesterID
                },
                DeliveryAddress = new Address()
                {
                    ID = serviceRequestPresentationModel.Address.ID
                }
            };
            return serviceRequest;
        }
    }
}
