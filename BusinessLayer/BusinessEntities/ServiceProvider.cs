using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceProvider : User
    {
        public string ID { get; set; }
        public double AverageScore { get; set; }
        public List<Review> Reviews { get; set; }
        public List<PriceRate> PriceRates { get; set; }
        public List<City> Cities { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }

        public List<ServiceProvider> FindMatches(Dictionary<string, string> queryParameters)
        {

            IRestRequest<ServiceProviderPaginationDTO> request = new RestRequest<ServiceProviderPaginationDTO>();
            ServiceProviderPaginationDTO response = request.GetAllWithPagination($"providers", true, queryParameters);

            List<ServiceProvider> serviceProviders =
                ServiceProviderMapper.CreateListOfServiceProviderFromServiceProviderOverviewItemDTO(response.Data);
            return serviceProviders;            


        }
    }
}
