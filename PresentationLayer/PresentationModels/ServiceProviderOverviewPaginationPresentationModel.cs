using System.Collections.Generic;

namespace PresentationLayer.PresentationModels
{
    public class ServiceProviderOverviewPaginationPresentationModel
    {
        public LinksPresentationModel Links { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public List<ServiceProviderOverviewItemPresentationModel> ServiceProvidersOverview { get; set; }
    }

    public class LinksPresentationModel
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Prev { get; set; }
        public string Next { get; set; }
    }
}
