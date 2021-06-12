using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class ServiceProviderMapper
    {

        public static List<ServiceProvider> CreateListOfServiceProviderFromServiceProviderOverviewItemDTO(ServiceProviderPaginationDTO serviceProviderPaginationDTO)
        {
            List<ServiceProvider> serviceProviderEntities = new List<ServiceProvider>();
            serviceProviderPaginationDTO.Data.ForEach(serviceProviderDTO =>
            {
                ServiceProvider serviceProviderEntity = new ServiceProvider
                {
                    ID = serviceProviderDTO.ID,
                    Names = serviceProviderDTO.Names,
                    Lastname = serviceProviderDTO.LastName,
                    AverageScore = serviceProviderDTO.AverageScore,
                    PriceRates = new List<PriceRate>
                    {
                        new PriceRate
                        {
                            Price = serviceProviderDTO.PriceRate
                        }
                    }
                };

                serviceProviderEntities.Add(serviceProviderEntity);
            });

            return serviceProviderEntities;
        }

        public static ServiceProvider CreateServiceProviderFromServiceProviderDetailDTO(ServiceProviderDetailDTO serviceProviderDetailDTO)
        {
            ServiceProvider serviceProvider = new ServiceProvider
            {
                ID = serviceProviderDetailDTO.ID,
                Names = serviceProviderDetailDTO.Names,
                Lastname = serviceProviderDetailDTO.LastName,
                EmailAddress = serviceProviderDetailDTO.EmailAddress,
                AverageScore = serviceProviderDetailDTO.AverageScore,
                PriceRates = PriceRateMapper.CreateListOfPriceRatesFromListOfPriceRatesDTO(serviceProviderDetailDTO.PriceRates)
            };
            return serviceProvider;
        }

        public static ServiceProvider CreateServiceProviderFromUserOverviewDTO(UserOverviewDTO userOverviewDTO)
        {
            ServiceProvider serviceProvider = new ServiceProvider
            {
                ID = userOverviewDTO.ID,
                Names = userOverviewDTO.Names,
                Lastname = userOverviewDTO.LastName
            };
            return serviceProvider;
        }
    }
}
