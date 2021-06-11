using FluentValidation;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.ValidationModules
{
    public class ServiceRequestPresentationModelValidator : AbstractValidator<ServiceRequestPresentationModel>
    {
        public ServiceRequestPresentationModelValidator()
        {
            RuleFor(serviceRequest => serviceRequest.KindOfService).InclusiveBetween(0, 4);
            RuleFor(serviceRequest => serviceRequest.Address).NotNull().NotEmpty();
            RuleFor(serviceRequest => serviceRequest.AdditionalDetails).MaximumLength(255).WithState(serviceRequest => "TextBoxAdditionalDetails");            
        }
    }
}
