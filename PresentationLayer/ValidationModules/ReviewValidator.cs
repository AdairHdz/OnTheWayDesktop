using FluentValidation;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.ValidationModules
{
    public class ReviewValidator : AbstractValidator<ReviewPresentationModel>
    {
        public ReviewValidator()
        {
            RuleFor(review => review.Details).Matches("^[A-z0-9 ]{0,255}$")
                .WithState(review => "TextBoxDetails");
            RuleFor(review => review.Title)
                .NotEmpty().WithState(review => "TextBoxTitle")
                .Matches("^[A-z0-9 ]{1,30}$").WithState(review => "TextBoxTitle");
            RuleFor(review => review.Score).InclusiveBetween(1, 5);
        }
    }
}
