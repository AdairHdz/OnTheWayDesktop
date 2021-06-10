using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class PriceRateMapper
    {
        public static PriceRate CreatePriceRateFromPriceRateDTO(PriceRateDTO priceRateDTO)
        {
            List<BusinessEntities.DayOfWeek> workingDays = new List<BusinessEntities.DayOfWeek>();
            priceRateDTO.WorkingDays.ForEach(workingDay =>
            {
                workingDays.Add((BusinessEntities.DayOfWeek)workingDay);
            });

            PriceRate priceRate = new PriceRate
            {
                ID = priceRateDTO.ID,
                StartingHour = priceRateDTO.StartingHour,
                EndingHour = priceRateDTO.EndingHour,
                Price = priceRateDTO.Price,
                KindOfService = (KindOfService)priceRateDTO.KindOfService,
                City = CityMapper.CreateCityEntityFromCityDTO(priceRateDTO.City),
                WorkingDays = workingDays
            };
            return priceRate;
        }

        public static List<PriceRate> CreateListOfPriceRatesFromListOfPriceRatesDTO(List<PriceRateDTO> priceRateDTOs)
        {            
            List<PriceRate> priceRates = new List<PriceRate>();
            if(priceRateDTOs == null)
            {
                return priceRates;
            }

            priceRateDTOs.ForEach(priceRateDTO =>
            {
                
                List<BusinessEntities.DayOfWeek> workingDays = new List<BusinessEntities.DayOfWeek>();
                priceRateDTO.WorkingDays.ForEach(workingDay =>
                {
                    workingDays.Add((BusinessEntities.DayOfWeek)workingDay);
                });

                PriceRate priceRate = new PriceRate
                {
                    ID = priceRateDTO.ID,
                    StartingHour = priceRateDTO.StartingHour,
                    EndingHour = priceRateDTO.EndingHour,
                    Price = priceRateDTO.Price,
                    KindOfService = (KindOfService)priceRateDTO.KindOfService,
                    City = CityMapper.CreateCityEntityFromCityDTO(priceRateDTO.City),
                    WorkingDays = workingDays
                };

                priceRates.Add(priceRate);
            });

            return priceRates;
        }
    }
}
