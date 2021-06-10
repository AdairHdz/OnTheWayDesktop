using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class City
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public List<City> GetCities(string stateID)
        {
            List<City> retrievedCities;
            RestRequest<CityDTO> restRequest = new RestRequest<CityDTO>(false);
            IRestResponse<List<CityDTO>> response = restRequest.GetAll($"states/{stateID}/cities");
            retrievedCities = CityMapper.CreateListOfCityEntitiesFromListOfCityDTO(response.Data);
            return retrievedCities;
        }
    }
}
