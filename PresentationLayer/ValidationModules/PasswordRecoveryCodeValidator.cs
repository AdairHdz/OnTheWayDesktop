using FluentValidation;
using PresentationLayer.PresentationModels;
using System.Text.RegularExpressions;

namespace PresentationLayer.ValidationModules
{
    public class PasswordRecoveryCodeValidator : AbstractValidator<PasswordRecoveryCodePresentationModel>
    {
        public PasswordRecoveryCodeValidator()
        {
            RuleFor(passwordRecovery => passwordRecovery.Password)
                .NotEmpty().WithState(passwordRecovery => "PasswordBoxPassword")
                .NotNull().WithState(passwordRecovery => "PasswordBoxPassword")
                .MinimumLength(8).WithState(passwordRecovery => "PasswordBoxPassword");
            RuleFor(passwordRecovery => passwordRecovery.Passwordconfirmation)
                .Equal(passwordRecovery => passwordRecovery.Password).WithState(passwordRecovery => "PasswordConfirmationBoxPassword");
            RuleFor(passwordRecovery => passwordRecovery.RecoveryCode)
                .NotEmpty().WithState(passwordRecovery => "TextBoxRecoveryCode")
                .NotNull().WithState(passwordRecovery => "TextBoxRecoveryCode")
                .Length(8).WithState(passwordRecovery => "TextBoxRecoveryCode");
            RuleFor(passwordRecovery => passwordRecovery.RecoveryEmail)
                .Must(BeAValidEmail).WithState(passwordRecovery => "TextBoxRecoveryEmail");
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
