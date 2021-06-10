using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;
using Utils;

namespace BusinessLayer.BusinessEntities
{
    public class Address
    {
        public string ID { get; set; }
        public string IndoorNumber { get; set; }
        public string OutdoorNumber { get; set; }        
        public string Street { get; set; }
        public string Suburb { get; set; }
        public City City { get; set; }

        public void Register()
        {
            AddressDTO addressDTO = AddressMapper.CreateAddressDTO(this);
            IRestRequest<AddressDTO> request = new RestRequest<AddressDTO>();
            request.CreateAsync($"requesters/{Session.GetSession().UserID}/addresses", addressDTO);            
        }

        public List<Address> GetAddresses()
        {
            List<Address> retrievedAddresses;
            RestRequest<AddressResponseDTO> restRequest = new RestRequest<AddressResponseDTO>();
            IRestResponse<List<AddressResponseDTO>> response = restRequest.GetAll($"/requesters/{Session.GetSession().UserID}/addresses");
            retrievedAddresses = AddressMapper.CreateListOfAddressEntitiesFromAddressDTOs(response.Data);
            return retrievedAddresses;            
        }
    }
}
