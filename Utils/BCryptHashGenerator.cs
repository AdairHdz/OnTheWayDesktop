namespace Utils
{
    public static class BCryptHashGenerator
    {        
        public static string GenerateHashedString(string stringToBeHashed)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(8);
            string hashedString = BCrypt.Net.BCrypt.HashPassword(stringToBeHashed, salt);
            return hashedString;
        }
    }
}
