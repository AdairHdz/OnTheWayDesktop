using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

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

        public static List<ServiceRequestHistoryPresentationModel> CreateServiceRequestHistoryPresentationModelListFromServiceRequestEntityList(
            List<ServiceRequest> serviceRequestEntitiesList)
        {
            List<ServiceRequestHistoryPresentationModel> serviceRequestHistories = new List<ServiceRequestHistoryPresentationModel>();
            serviceRequestEntitiesList.ForEach(serviceRequestItem =>
            {
                ServiceRequestHistoryPresentationModel serviceRequestHistory = new ServiceRequestHistoryPresentationModel
                {                    
                    ID = serviceRequestItem.ID,
                    KindOfService = CreateKindOfService(serviceRequestItem.KindOfService),
                    Cost = serviceRequestItem.Cost,
                    Status = CreateServiceStatus(serviceRequestItem.ServiceStatus),
                    ServiceProvider = $"{serviceRequestItem.ServiceProvider.Names} {serviceRequestItem.ServiceProvider.Lastname}"
                };
                serviceRequestHistories.Add(serviceRequestHistory);
            });
            return serviceRequestHistories;
        }

        private static string CreateServiceStatus(ServiceStatus serviceStatus)
        {
            string statusName;
            switch(serviceStatus)
            {
                case ServiceStatus.Active:
                    statusName = "Activo";
                    break;
                case ServiceStatus.Canceled:
                    statusName = "Cancelado";
                    break;
                case ServiceStatus.Concretized:
                    statusName = "Completado";
                    break;
                case ServiceStatus.PendingOfAcceptance:
                    statusName = "Pendiente de aceptación";
                    break;
                default:
                    statusName = "Desconocido";
                    break;
            }
            return statusName;
        }

        private static string CreateKindOfService(KindOfService kindOfService)
        {
            string kindOfServiceName;
            switch (kindOfService)
            {
                case KindOfService.ServicePayment:
                    kindOfServiceName = "Pago de servicios";
                    break;
                case KindOfService.Delivery:
                    kindOfServiceName = "Entrega";
                    break;
                case KindOfService.DrugPurchase:
                    kindOfServiceName = "Compra de fármacos";
                    break;
                case KindOfService.GroceryShopping:
                    kindOfServiceName = "Compra de víveres";
                    break;
                default:
                    kindOfServiceName = "Otro";
                    break;
            }
            return kindOfServiceName;
        }
    }
}
