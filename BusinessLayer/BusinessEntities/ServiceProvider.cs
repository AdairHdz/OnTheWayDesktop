using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using Utils.CustomExceptions;

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
            
            try
            {
                IRestRequest<ServiceProviderOverviewItemDTO> request = new RestRequest<ServiceProviderOverviewItemDTO>();
                IRestRequest queryParams = new RestRequest();

                queryParams.AddQueryParameter("maxPriceRate", queryParameters["maxPriceRate"]);
                queryParams.AddQueryParameter("city", queryParameters["city"]);
                queryParams.AddQueryParameter("kindOfService", queryParameters["kindOfService"]);
                IRestResponse<List<ServiceProviderOverviewItemDTO>> response = request.GetAll("providers", queryParams);

                List<ServiceProvider> serviceProviders =
                    ServiceProviderMapper.CreateListOfServiceProviderFromServiceProviderOverviewItemDTO(response.Data);
                return serviceProviders;
            }
            catch (EmptyCollectionException)
            {
                throw new EmptyCollectionException();
            }
            
            
        }
    }
}
