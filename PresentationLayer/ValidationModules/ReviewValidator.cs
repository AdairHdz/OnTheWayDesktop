using BusinessLayer.BusinessEntities;
using FluentValidation;

namespace PresentationLayer.ValidationModules
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(review => review.Details).Matches("^[A-z0-9 ]{0,255}$");
            RuleFor(review => review.Title).NotEmpty().Matches("^[A-z0-9 ]{1,30}$");
        }
    }
}
