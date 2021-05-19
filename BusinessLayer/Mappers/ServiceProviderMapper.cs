using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class ServiceProviderMapper
    {

        public static List<ServiceProvider> CreateListOfServiceProviderFromServiceProviderOverviewItemDTO(List<ServiceProviderOverviewItemDTO> serviceProviders)
        {
            List<ServiceProvider> serviceProviderEntities = new List<ServiceProvider>();
            serviceProviders.ForEach(serviceProviderDTO =>
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
    }
}
