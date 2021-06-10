using DataLayer.DataTransferObjects;
using BusinessLayer.Mappers;
using DataLayer;
using RestSharp;
using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class State
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }

        public List<State> GetStates()
        {
            List<State> retrievedStates;            
            RestRequest<StateDTO> restRequest = new RestRequest<StateDTO>(false);
            IRestResponse<List<StateDTO>> response = restRequest.GetAll("states");
            retrievedStates = StateMapper.CreateStatesList(response.Data);
            return retrievedStates;
        }
    }
}
