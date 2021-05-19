using RestSharp;
using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceProvider : User
    {
        public string ID { get; set; }
        public double AverageScore { get; set; }
        //public string IdentityPicture { get; set; }
        public List<Review> Reviews { get; set; }
        public List<PriceRate> PriceRates { get; set; }
        public List<City> Cities { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }

        public List<ServiceProvider> FindMatches(double maxPriceRate, string cityName, object kindOfService)
        {
            //RestRequest<ServiceProvider> restRequest = new RestRequest<ServiceProvider>();
            IRestRequest request = new RestRequest("providers", Method.POST);
            request.AddQueryParameter("maxPriceRate", ""+maxPriceRate);
            request.AddQueryParameter("city", cityName);
            request.AddQueryParameter("kindOfService", ""+kindOfService);

            return null;
        }
    }
}
