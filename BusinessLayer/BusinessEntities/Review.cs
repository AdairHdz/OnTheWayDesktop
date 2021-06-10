using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System;
using System.Collections.Generic;

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
        public List<string> Evidence { get; set; }

        public List<Review> FindAll(string serviceProviderID, Dictionary<string, string> queryParameters)
        {
            List<Review> retrievedReviews;
            RestRequest<ReviewPaginationDTO> restRequest = new RestRequest<ReviewPaginationDTO>();
            var response = restRequest.GetAllWithPagination($"/providers/{serviceProviderID}/reviews", true, queryParameters);
            retrievedReviews = ReviewMapper.CreateListOfReviewEntitiesFromListOfReviewDTO(response.Data);
            return retrievedReviews;
        }
    }
}
