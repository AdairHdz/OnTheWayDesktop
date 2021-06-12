using System;

namespace DataLayer.Helpers
{
    public class CustomClaims
    {
        public int Exp { get; set; }
        public UserInfo UserInfo { get; set; }

        public bool TokenHasExpired()
        {
            var tokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(Exp).ToLocalTime();            
            double remainingTime = (tokenExpirationDate - DateTime.Now).TotalMinutes;
            return remainingTime <= 1;
        }
    }
}
