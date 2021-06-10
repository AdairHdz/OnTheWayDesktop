using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;
using Utils.CustomExceptions;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceProvider : User
    {
        public string ID { get; set; }
        public int AverageScore { get; set; }
        public List<Review> Reviews { get; set; }
        public List<PriceRate> PriceRates { get; set; }
        public List<City> Cities { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }

        public List<ServiceProvider> FindMatches(Dictionary<string, string> queryParameters)
        {
            
            try
            {
                var maxPriceRate = queryParameters["maxPriceRate"];
                var cityName = queryParameters["city"];
                var kindOfService = queryParameters["kindOfService"];
                IRestRequest<ServiceProviderOverviewItemDTO> request = new RestRequest<ServiceProviderOverviewItemDTO>();
                IRestResponse<List<ServiceProviderOverviewItemDTO>> response = request.GetAll($"providers?maxPriceRate={maxPriceRate}&city={cityName}&kindOfService={kindOfService}");

                List<ServiceProvider> serviceProviders =
                    ServiceProviderMapper.CreateListOfServiceProviderFromServiceProviderOverviewItemDTO(response.Data);
                return serviceProviders;
            }
            catch (EmptyCollectionException)
            {
                throw new EmptyCollectionException();
            }
        }

        public ServiceProvider Find(string serviceProviderID)
        {
            try
            {
                IRestRequest<ServiceProviderDetailDTO> request = new RestRequest<ServiceProviderDetailDTO>();
                IRestResponse<ServiceProviderDetailDTO> response = request.Get($"providers/{serviceProviderID}");

                ServiceProvider serviceProvider = ServiceProviderMapper.CreateServiceProviderFromServiceProviderDetailDTO(response.Data);
                return serviceProvider;
            }
            catch (EmptyCollectionException)
            {
                throw new EmptyCollectionException();
            }
            
        }
    }
}
