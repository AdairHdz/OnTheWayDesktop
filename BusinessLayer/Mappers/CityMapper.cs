using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class CityMapper
    {
        public static List<City> CreateListOfCityEntitiesFromListOfCityDTO(List<CityDTO> citiesDTOList)
        {
            List<City> citiesEntities = new List<City>();
            citiesDTOList.ForEach(cityDTO =>
            {
                City city = new City
                {
                    ID = cityDTO.ID,
                    Name = cityDTO.Name
                };

                citiesEntities.Add(city);
            });

            return citiesEntities;
        }

        public static City CreateCityEntityFromCityDTO(CityDTO cityDTO)
        {
            City city = new City
            {
                ID = cityDTO.ID,
                Name = cityDTO.Name
            };
            return city;
        }
    }
}
