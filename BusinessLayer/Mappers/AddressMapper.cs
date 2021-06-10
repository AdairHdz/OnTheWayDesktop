using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class AddressMapper
    {
        public static AddressDTO CreateAddressDTO(Address address)
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

        public static List<Address> CreateListOfAddressEntitiesFromAddressDTOs(List<AddressResponseDTO> listOfAddressDTOs)
        {
            List<Address> listOfAddresses = new List<Address>();
            listOfAddressDTOs.ForEach(addressDTO =>
            {
                Address addressElement = new Address
                {        
                    ID = addressDTO.ID,
                    IndoorNumber = addressDTO.IndoorNumber,
                    OutdoorNumber = addressDTO.OutdoorNumber,
                    Street = addressDTO.Street,
                    Suburb = addressDTO.Suburb,
                    City = CityMapper.CreateCityEntityFromCityDTO(addressDTO.City)
                };

                listOfAddresses.Add(addressElement);
            });
            return listOfAddresses;
        }
    }
}
