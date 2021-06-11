using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class AddressMapper
    {
        public static AddressDTO CreateAddressDTOFromEntity(Address address)
        {
            AddressDTO addressDTO = new AddressDTO
            {
                IndoorNumber = address.IndoorNumber,
                OutdoorNumber = address.OutdoorNumber,
                Street = address.Street,
                Suburb = address.Suburb,
                CityID = address.City.ID
            };
            return addressDTO;
        }

        public static List<Address> CreateListOfAddressEntitiesFromListOfAddressDTOs(List<AddressDetailsDTO> addressDTOList)
        {
            List<Address> addressEntitiesList = new List<Address>();
            addressDTOList.ForEach(addressDTOElement =>
            {
                Address addressEntity = new Address
                {
                    ID = addressDTOElement.ID,
                    IndoorNumber = addressDTOElement.IndoorNumber,
                    OutdoorNumber = addressDTOElement.OutdoorNumber,
                    Street = addressDTOElement.Street,
                    Suburb = addressDTOElement.Suburb,
                    City = CityMapper.CreateCityEntityFromCityDTO(addressDTOElement.City)
                };
                addressEntitiesList.Add(addressEntity);
            });
            return addressEntitiesList;
        }
    }
}
