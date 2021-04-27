using BusinessLayer.BusinessEntities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace PresentationLayer.ValidationModules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Names).NotEmpty().Must(BeAValidName);
            RuleFor(user => user.Lastname).NotEmpty().Must(BeAValidLastName);
            RuleFor(user => user.EmailAddress).Must(BeAValidEmail);
            RuleFor(user => user.Password).Must(BeAValidPassword);
        }

        private bool BeAValidName(string name)
        {
            if (name == null || name == "")
            {
                return false;
            }

            string sanitizedName = name.Trim();
            Regex regularExpression = new Regex("^[A-z ]{1,30}$");
            bool hasValidFormat = regularExpression.IsMatch(sanitizedName);            
            return hasValidFormat;
        }

        private bool BeAValidLastName(string lastName)
        {
            if(lastName == null || lastName == "")
            {
                return false;
            }
            string sanitizedLastname = lastName.Trim();
            Regex regularExpression = new Regex("^[A-z ]{1,60}$");
            bool hasValidFormat = regularExpression.IsMatch(sanitizedLastname);            
            return hasValidFormat;
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

        public bool BeAValidPassword(string password)
        {
            if(HasAtLeastOneSpecialCharacter(password) && HasAtLeastOneCapitalLetter(password)
                && HasAtLeastOneLowercaseLetter(password) && IsBetween8And50CharactersLength(password)
                && HasAtLeastOneNumericCharacter(password))
            {
                return true;
            }
            return false;
        }
        private bool HasAtLeastOneSpecialCharacter(string password)
        {
            Regex regularExpression = new Regex("\\W+");
            MatchCollection matches = regularExpression.Matches(password);
            return matches.Count >= 1;
        }

        private bool HasAtLeastOneCapitalLetter(string password)
        {
            Regex regularExpression = new Regex("[A-Z+]");
            MatchCollection matches = regularExpression.Matches(password);
            return matches.Count >= 1;
        }

        private bool HasAtLeastOneNumericCharacter(string password)
        {
            Regex regularExpression = new Regex("[0-9]");
            MatchCollection matches = regularExpression.Matches(password);
            return matches.Count >= 1;
        }

        private bool HasAtLeastOneLowercaseLetter(string password)
        {
            Regex regularExpression = new Regex("[a-z+]");
            MatchCollection matches = regularExpression.Matches(password);
            return matches.Count >= 1;
        }

        private bool IsBetween8And50CharactersLength(string password)
        {
            return password.Length >= 8 && password.Length <= 50;
        }
    }
}
