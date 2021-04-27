using System.Collections.Generic;

namespace BusinessLayer.BusinessEntities
{
    public class ServiceProvider : User
    {
        public double AverageScore { get; set; }
        public string IdentityPicture { get; set; }
        public List<Review> Reviews { get; set; }
        public List<City> Cities { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
    }
}
