using FluentValidation;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.ValidationModules
{
    public class AccountVerificationCodeValidator : AbstractValidator<AccountVerificationCodePresentationModel>
    {
        public AccountVerificationCodeValidator()
        {
            RuleFor(accountVerification => accountVerification.VerificationCode)
                .NotNull().WithState(login => "TextBoxVerificationCode")
                .NotEmpty().WithState(login => "TextBoxVerificationCode")
                .Length(8).WithState(login => "TextBoxVerificationCode");
        }
    }
}
