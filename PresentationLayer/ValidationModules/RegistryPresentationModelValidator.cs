using FluentValidation;
using PresentationLayer.PresentationModels;
using System.Text.RegularExpressions;

namespace PresentationLayer.ValidationModules
{
    public class RegistryPresentationModelValidator : AbstractValidator<RegistryPresentationModel>
    {
        public RegistryPresentationModelValidator()
        {
            RuleFor(registry => registry.Names).Matches("^[A-z ]{1,50}$").WithState(registry => "TextBoxNames").NotEmpty()
                .WithState(registry => "TextBoxNames");
            RuleFor(registry => registry.LastName).Matches("^[A-z ]{1,50}$").WithState(registry => "TextBoxLastName").NotEmpty()
                .WithState(registry => "TextBoxLastName");
            RuleFor(registry => registry.EmailAddress).Must(BeAValidEmail).WithState(registry => "TextBoxEmailAddress");
            RuleFor(registry => registry.Password).NotEmpty().WithState(registry => "PasswordBoxPassword");
            RuleFor(registry => registry.State).NotNull();
        }

        private bool BeAValidEmail(string email)
        {
            Regex regularExpression = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            if (email == null || email.Length == 0)
            {
                return false;
            }

            bool hasValidFormat = regularExpression.IsMatch(email);
            bool hasValidLength = email.Length <= 254;
            return hasValidFormat && hasValidLength;
        }
    }
}
