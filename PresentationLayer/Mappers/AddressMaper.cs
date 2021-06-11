
using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public class AddressMapper
    {
        public static Address CreateAddressEntity(AddressPresentationModel addressPresentationModel)
        {
            Address address = new Address
            {
                IndoorNumber = addressPresentationModel.IndoorNumber,
                OutdoorNumber = addressPresentationModel.OutdoorNumber,
                Street = addressPresentationModel.Street,
                Suburb = addressPresentationModel.Suburb,
                City = CityMapper.CreateCityEntityFromCityPresentationModel(addressPresentationModel.City)
            };

            return address;
        }

        public static List<AddressPresentationModel> CreateAddressPresentationModelList(List<Address> addressEntitiesList)
        {
            List<AddressPresentationModel> addressPresentationModelsList = new List<AddressPresentationModel>();
            addressEntitiesList.ForEach(addressEntity =>
            {
                AddressPresentationModel addressPresentationModel = new AddressPresentationModel
                {
                    ID = addressEntity.ID,
                    IndoorNumber = addressEntity.IndoorNumber,
                    OutdoorNumber = addressEntity.OutdoorNumber,
                    Street = addressEntity.Street,
                    Suburb = addressEntity.Suburb,
                    City = CityMapper.CreateCityPresentationModelFromCityEntity(addressEntity.City),
                    AddressOverview = CreateAddressOverview(addressEntity),
                };
                addressPresentationModelsList.Add(addressPresentationModel);
            });
            
            return addressPresentationModelsList;
        }

        private static string CreateAddressOverview(Address address)
        {
            string addressOverview = $"{address.Street} #{address.OutdoorNumber}";
            if (address.IndoorNumber != null && address.IndoorNumber != "")
            {
                addressOverview += $", Interior {address.IndoorNumber}";
            }
            addressOverview += $", col. {address.Suburb}; {address.City.Name}";
            return addressOverview;
        }
    }
}