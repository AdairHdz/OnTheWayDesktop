using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Mappers
{
    public class PriceRateMapper
    {
        public static List<PriceRatePresentationModel> CreateListOfPriceRatePresentationModel(List<PriceRate> priceRates)
        {
            List<PriceRatePresentationModel> priceRatePresentationModels = new List<PriceRatePresentationModel>();
            priceRates.ForEach(priceRateElement =>
            {                
                PriceRatePresentationModel priceRatePresentationModel = new PriceRatePresentationModel
                {                    
                    StartingHour = priceRateElement.StartingHour,
                    EndingHour = priceRateElement.EndingHour,
                    Price = priceRateElement.Price,
                    KindOfService = CreateKindOfService(priceRateElement.KindOfService),
                    City = priceRateElement.City.Name,
                    WorkingDays = CreateListOfWorkingDays(priceRateElement.WorkingDays),                    
                };                
                priceRatePresentationModels.Add(priceRatePresentationModel);
            });
            return priceRatePresentationModels;
        }

        private static string CreateKindOfService(KindOfService kindOfService)
        {
            string kindOfServiceName;
            switch (kindOfService)
            {
                case KindOfService.ServicePayment:
                    kindOfServiceName = "Pago de servicios";
                    break;
                case KindOfService.Delivery:
                    kindOfServiceName = "Entrega";
                    break;
                case KindOfService.DrugPurchase:
                    kindOfServiceName = "Compra de fármacos";
                    break;
                case KindOfService.GroceryShopping:
                    kindOfServiceName = "Compra de víveres";
                    break;
                default:
                    kindOfServiceName = "Otro";
                    break;
            }                    
            return kindOfServiceName;
        }

        private static List<string> CreateListOfWorkingDays(List<BusinessLayer.BusinessEntities.DayOfWeek> daysOfWeek)
        {
            List<string> listOfDaysOfWeekNames = new List<string>();
            daysOfWeek.ForEach(dayOfWeek =>
            {
                string dayName;
                switch (dayOfWeek)
                {
                    case BusinessLayer.BusinessEntities.DayOfWeek.Monday:
                        dayName = "Lunes";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Tuesday:
                        dayName = "Martes";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Wednesday:
                        dayName = "Miércoles";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Thursday:
                        dayName = "Jueves";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Friday:
                        dayName = "Viernes";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Saturday:
                        dayName = "Sábado";
                        break;
                    case BusinessLayer.BusinessEntities.DayOfWeek.Sunday:
                        dayName = "Domingo";
                        break;
                    default:
                        dayName = "Error al recuperar el día";
                        break;
                }
                listOfDaysOfWeekNames.Add(dayName);
            });
            return listOfDaysOfWeekNames;
        }
    }
}
