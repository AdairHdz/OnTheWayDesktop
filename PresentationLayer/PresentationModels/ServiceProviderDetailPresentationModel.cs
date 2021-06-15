using System.Collections.Generic;

namespace PresentationLayer.PresentationModels
{
    public class ServiceProviderDetailPresentationModel
    {
        public string FullName { get; set; }
        public int AverageScore { get; set; }
        public List<PriceRatePresentationModel> PriceRates { get; set; }
        public string ProfileImage { get; set; }
    }
}