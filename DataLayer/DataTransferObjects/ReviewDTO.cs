using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class ReviewDTO
    {        
        public string Title { get; set; }
        public string Details { get; set; }
        public int Score { get; set; }
        public List<EvidenceDTO> Evidence { get; set; }
        public string ServiceRequesterID { get; set; }
    }
}
