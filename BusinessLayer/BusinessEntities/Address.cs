using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
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

        public List<Address> FindAll()
        {
            RestRequest<AddressDetailsDTO> request = new RestRequest<AddressDetailsDTO>();
            var response = request.GetAll($"requesters/{Session.GetSession().ID}/addresses");
            return AddressMapper.CreateListOfAddressEntitiesFromListOfAddressDTOs(response);
        }

        public void Register()
        {
            RestRequest<object> request = new RestRequest<object>();
            AddressDTO addressDTO = AddressMapper.CreateAddressDTOFromEntity(this);
            request.Post($"requesters/{Session.GetSession().ID}/addresses", addressDTO);
        }


    }
}
