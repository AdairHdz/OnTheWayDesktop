using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessEntities
{
    public class Review
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DateOfReview { get; set; }
        public int Score { get; set; }        
        public ServiceProvider ServiceProvider { get; set; } 
        public ServiceRequester ServiceRequester { get; set; }
        public List<Evidence> Evidence { get; set; }

        public List<Review> FindAll(string serviceProviderID, Dictionary<string, string> queryParameters)
        {
            List<Review> retrievedReviews;
            RestRequest<ReviewPaginationDTO> restRequest = new RestRequest<ReviewPaginationDTO>();
            var response = restRequest.GetAllWithPagination($"/providers/{serviceProviderID}/reviews", true, queryParameters);
            retrievedReviews = ReviewMapper.CreateListOfReviewEntitiesFromListOfReviewDTO(response.Data);
            return retrievedReviews;
        }

        public void Save()
        {
            ReviewDTO reviewDTO = ReviewMapper.CreateReviewDTOFromReviewEntity(this);
            RestRequest<ReviewResponseDTO> request = new RestRequest<ReviewResponseDTO>();
            var response = request.Post($"providers/{ServiceProvider.ID}/reviews", reviewDTO);
            this.ID = response.ID;
        }

        public async Task SaveReviewFilesAsync()
        {
            RestRequest<object> request = new RestRequest<object>();
            List<string> files = new List<string>();
            Evidence.ForEach(evidence =>
            {
                files.Add(evidence.Name);
            });
            await request.PostFilesAsync($"providers/{ServiceProvider.ID}/reviews/{ID}/evidence", files);
        }
    }
}
