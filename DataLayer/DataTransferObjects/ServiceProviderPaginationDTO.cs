using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class ServiceProviderPaginationDTO
    {
        public LinksDTO Links { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public List<ServiceProviderOverviewItemDTO> Data { get; set; }
    }
}
