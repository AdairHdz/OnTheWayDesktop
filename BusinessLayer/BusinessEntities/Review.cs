using System;

namespace BusinessLayer.BusinessEntities
{
    public class Review
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DateOfReview { get; set; }
        public int Score { get; set; }        
        public ServiceProvider ServiceProvider { get; set; }
    }
}
