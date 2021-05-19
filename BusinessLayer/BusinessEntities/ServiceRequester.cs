using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceRequester : User 
    {
        public string ID { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
        public List<Address> Addresses { get; set; }


    }


}
