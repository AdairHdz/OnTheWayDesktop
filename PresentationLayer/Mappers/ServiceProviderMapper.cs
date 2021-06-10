using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public class ServiceProviderMapper
    {
        public static List<ServiceProviderOverviewItemPresentationModel> CreateListOfServiceProviderOverviewItemDTO(List<ServiceProvider> serviceProviders)
        {
            List<ServiceProviderOverviewItemPresentationModel> serviceProviderOverviewItems = new List<ServiceProviderOverviewItemPresentationModel>();
            serviceProviders.ForEach(serviceProvider =>
            {
                ServiceProviderOverviewItemPresentationModel serviceProviderItem = new ServiceProviderOverviewItemPresentationModel
                {
                    ID = serviceProvider.ID,
                    Name = serviceProvider.Names + " " + serviceProvider.Lastname,
                    AverageScore = (int)serviceProvider.AverageScore,
                    PriceRate = serviceProvider.PriceRates[0].Price
                };

                serviceProviderOverviewItems.Add(serviceProviderItem);
            });
            return serviceProviderOverviewItems;
        }

        public static ServiceProviderDetailPresentationModel CreateServiceProviderPresentationModelFromEntity(ServiceProvider serviceProvider)
        {
            ServiceProviderDetailPresentationModel serviceProviderDetailPresentationModel = new ServiceProviderDetailPresentationModel
            {
                FullName = serviceProvider.Names + " " + serviceProvider.Lastname,
                AverageScore = serviceProvider.AverageScore,
                PriceRates = PriceRateMapper.CreateListOfPriceRatePresentationModel(serviceProvider.PriceRates)
            };
            return serviceProviderDetailPresentationModel;
        }
    }
}
