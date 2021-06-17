using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;
using System.Collections.Generic;

namespace BusinessLayer.Mappers
{
    public static class ReviewMapper
    {
        public static List<Review> CreateListOfReviewEntitiesFromListOfReviewDTO(List<ReviewResponseDTO> reviewDTOs)
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

        private static List<Evidence> CreateListOfEvidence(List<EvidenceResponseDTO> listEvidenceDTO)
        {
            List<Evidence> listOfEvidence = new List<Evidence>();
            if(listEvidenceDTO != null)
            {
                listEvidenceDTO.ForEach(evidenceElement =>
                {
                    Evidence evidence = new Evidence
                    {
                        Link = evidenceElement.Link,
                        Name = evidenceElement.Name
                    };
                    listOfEvidence.Add(evidence);
                });
            }            
            return listOfEvidence;
        }

        private static List<EvidenceDTO> CreateListOfEvidenceDTO(List<Evidence> listOfEvidence)
        {
            List<EvidenceDTO> listOfEvidenceDTO = new List<EvidenceDTO>();
            if (listOfEvidence.Count > 0)
            {
                listOfEvidence.ForEach(evidenceElement =>
                {
                    EvidenceDTO evidenceDTO = new EvidenceDTO
                    {
                        Name = evidenceElement.Name
                    };
                    listOfEvidenceDTO.Add(evidenceDTO);
                });
            }
            return listOfEvidenceDTO;
        }

        public static ReviewDTO CreateReviewDTOFromReviewEntity(Review reviewEntity)
        {
            ReviewDTO reviewDTO = new ReviewDTO
            {                
                Title = reviewEntity.Title,
                Details = reviewEntity.Details,
                Score = reviewEntity.Score,
                Evidence = CreateListOfEvidenceDTO(reviewEntity.Evidence),
                ServiceRequesterID = reviewEntity.ServiceRequester.ID
            };
            return reviewDTO;
        }

        
    }
}