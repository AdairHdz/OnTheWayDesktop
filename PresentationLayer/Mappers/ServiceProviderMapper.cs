using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public static class ServiceProviderMapper
    {
        public static ServiceProviderOverviewPaginationPresentationModel CreateListOfServiceProviderOverviewPagination(ServiceProviderPaginationDTO serviceProviderPaginationDTO)
        {
            ServiceProviderOverviewPaginationPresentationModel serviceProviderOverviewPaginationPresentationModel =
                new ServiceProviderOverviewPaginationPresentationModel
                {
                    Links = new LinksPresentationModel
                    {
                        First = serviceProviderPaginationDTO.Links.First,
                        Last = serviceProviderPaginationDTO.Links.Last,
                        Next = serviceProviderPaginationDTO.Links.Next,
                        Prev = serviceProviderPaginationDTO.Links.Prev
                    },
                    Total = serviceProviderPaginationDTO.Total,
                    Page = serviceProviderPaginationDTO.Page,
                    Pages = serviceProviderPaginationDTO.Pages,                    
                };
            
            List<ServiceProviderOverviewItemPresentationModel> serviceProviderOverviewItems = new List<ServiceProviderOverviewItemPresentationModel>();
            serviceProviderPaginationDTO.Data.ForEach(serviceProvider =>
            {
                ServiceProviderOverviewItemPresentationModel serviceProviderItem = new ServiceProviderOverviewItemPresentationModel
                {
                    ID = serviceProvider.ID,
                    Name = serviceProvider.Names + " " + serviceProvider.LastName,
                    AverageScore = serviceProvider.AverageScore,
                    PriceRate = serviceProvider.PriceRate
                };

                serviceProviderOverviewItems.Add(serviceProviderItem);
            });

            serviceProviderOverviewPaginationPresentationModel.ServiceProvidersOverview = serviceProviderOverviewItems;
            return serviceProviderOverviewPaginationPresentationModel;
        }
        

        public static ServiceProviderDetailPresentationModel CreateServiceProviderPresentationModelFromEntity(ServiceProvider serviceProvider)
        {
            ServiceProviderDetailPresentationModel serviceProviderDetailPresentationModel = new ServiceProviderDetailPresentationModel
            {
                FullName = serviceProvider.Names + " " + serviceProvider.Lastname,
                AverageScore = serviceProvider.AverageScore,
                PriceRates = PriceRateMapper.CreateListOfPriceRatePresentationModel(serviceProvider.PriceRates),
                ProfileImage = serviceProvider.ProfileImage
            };
            return serviceProviderDetailPresentationModel;
        }
    }
}
