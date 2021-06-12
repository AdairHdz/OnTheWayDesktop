using System.Collections.Generic;

namespace PresentationLayer.PresentationModels
{
    public class ReviewPresentationModel
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public int Score { get; set; }
        public List<string> Evidence { get; set; }
        public string ServiceProviderID { get; set; }
        public string ServiceRequesterID { get; set; }
    }
}
