using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public class CityMapper
    {
        public static List<CityPresentationModel> CreateListOfCityPresentationModels(List<City> cities)
        {
            List<CityPresentationModel> citiesPresentationModels = new List<CityPresentationModel>();
            cities.ForEach(city =>
            {
                CityPresentationModel cityPresentationModel = new CityPresentationModel
                {
                    ID = city.ID,
                    Name = city.Name
                };

                citiesPresentationModels.Add(cityPresentationModel);
            });
            return citiesPresentationModels;
        }

        public static City CreateCityEntityFromCityPresentationModel(CityPresentationModel cityPresentationModel)
        {
            City city = new City
            {
                ID = cityPresentationModel.ID,
                Name = cityPresentationModel.Name
            };
            return city;
        }
    }
}
