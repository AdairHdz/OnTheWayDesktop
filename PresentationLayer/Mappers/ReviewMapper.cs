using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.Mappers
{
    public class ReviewMapper
    {
        public static Review CreateReviewEntityFromReviewPresentationModel(ReviewPresentationModel reviewPresentationModel)
        {
            Review review = new Review
            {
                Title = reviewPresentationModel.Title,
                Details = reviewPresentationModel.Details,
                Score = reviewPresentationModel.Score,
                ServiceProvider = new ServiceProvider
                {
                    ID = reviewPresentationModel.ServiceProviderID
                },
                ServiceRequester = new ServiceRequester
                {
                    ID = reviewPresentationModel.ServiceRequesterID
                },
                Evidence = new System.Collections.Generic.List<Evidence>()
            };

            reviewPresentationModel.Evidence.ForEach(evidence =>
            {
                review.Evidence.Add(new Evidence
                {
                    Name = evidence
                });
            });
            return review;
        }
    }
}
