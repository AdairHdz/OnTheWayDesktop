using BusinessLayer.BusinessEntities;
using FluentValidation;

namespace PresentationLayer.ValidationModules
{
    public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceRequestValidator()
        {
            RuleFor(serviceRequest => serviceRequest.KindOfService).IsInEnum();
            RuleFor(serviceRequest => serviceRequest.DeliveryAddress).NotNull();
            RuleFor(serviceRequest => serviceRequest.Description).MaximumLength(255);
        }
    }
}
