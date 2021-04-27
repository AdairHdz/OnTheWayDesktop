using BusinessLayer.BusinessEntities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace PresentationLayer.ValidationModules
{
    public class UserLoginValidator : AbstractValidator<User>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.EmailAddress).Must(BeAValidEmail);
            RuleFor(user => user.Password).NotEmpty();
        }

        public bool BeAValidEmail(string email)
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
