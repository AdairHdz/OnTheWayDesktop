using FluentValidation;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.ValidationModules
{
    public class AddressPresentationModelValidator : AbstractValidator<AddressPresentationModel>
    {
        public AddressPresentationModelValidator()
        {
            RuleFor(addressPresentationModel => addressPresentationModel.IndoorNumber).MaximumLength(8).WithState(address => "TextBoxIndoorNumber");
            RuleFor(addressPresentationModel => addressPresentationModel.OutdoorNumber).NotEmpty().WithState(address => "TextBoxOutdoorNumber").MaximumLength(8).WithState(address => "TextBoxOutdoorNumber");
            RuleFor(addressPresentationModel => addressPresentationModel.Street).NotEmpty().WithState(address => "TextBoxStreet").MaximumLength(50).WithState(address => "TextBoxStreet");
            RuleFor(addressPresentationModel => addressPresentationModel.Suburb).NotEmpty().WithState(address => "TextBoxSuburb").MaximumLength(50).WithState(address => "TextBoxSuburb");
            RuleFor(addressPresentationModel => addressPresentationModel.City).NotNull().WithState(address => "ComboBoxCity");
        }
    }
}