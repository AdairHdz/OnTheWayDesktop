using System;
using System.Collections.Generic;

namespace DataLayer.DataTransferObjects
{
    public class ReviewResponseDTO
    {
        public string ID { get; set; }
        public DateTime DateOfReview { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Score { get; set; }
        public List<EvidenceResponseDTO> Evidence { get; set; }
        public UserOverviewDTO ServiceRequester { get; set; }
    }


}