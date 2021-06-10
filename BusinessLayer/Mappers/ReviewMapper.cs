using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public class ReviewMapper
    {
        public static List<Review> CreateListOfReviewEntitiesFromListOfReviewDTO(List<ReviewDTO> reviewDTOs)
        {
            List<Review> reviews = new List<Review>();

            reviewDTOs.ForEach(reviewDTO =>
            {
                Review reviewEntity = new Review
                {
                    ID = reviewDTO.ID,
                    Title = reviewDTO.Title,
                    Details = reviewDTO.Details,
                    DateOfReview = reviewDTO.DateOfReview,
                    Score = reviewDTO.Score,
                    ServiceRequester = ServiceRequesterMapper.CreateServiceRequesterFromUserOverviewDTO(reviewDTO.ServiceRequester),
                    Evidence = CreateListOfEvidence(reviewDTO.Evidence)
                };
                reviews.Add(reviewEntity);
            });
            return reviews;
        }

        private static List<string> CreateListOfEvidence(List<EvidenceDTO> listEvidenceDTO)
        {
            List<string> listOfEvidence = new List<string>();
            if(listOfEvidence.Count > 0)
            {
                listEvidenceDTO.ForEach(evidenceElement =>
                {
                    listOfEvidence.Add(evidenceElement.Name);
                });
            }            
            return listOfEvidence;
        }
    }
}