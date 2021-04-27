using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceRequester : User 
    {
        public List<ServiceRequest> ServiceRequests { get; set; }
    }
}
