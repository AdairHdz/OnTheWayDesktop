using FluentValidation;
using PresentationLayer.PresentationModels;
using System.Text.RegularExpressions;

namespace PresentationLayer.ValidationModules
{
    public class EmailAddressValidator : AbstractValidator<EmailAddressPresentationModel>
    {
        public EmailAddressValidator()
        {
            RuleFor(emailAddress => emailAddress.EmailAddress).Must(BeAValidEmail).WithState(registry => "TextBoxEmailAddress");
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
