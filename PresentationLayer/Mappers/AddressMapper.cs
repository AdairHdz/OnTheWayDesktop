using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;

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
    }
}
