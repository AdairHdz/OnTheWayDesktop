namespace Utils
{
    public class Session
    {
        private static Session _session;

        public string ID { get; set; }
        public string UserID { get; set; }
        public int UserType { get; set; }
        public bool Verified { get; set; }
        public string EmailAddress { get; set; }
        public string StateID { get; set; }
        public string AuthorizationToken { get; set; }
        public string RefreshToken { get; set; }

        private Session() { }

        public static Session GetSession()
        {
            if (_session == null)
            {
                _session = new Session();
            }
            return _session;
        }       
        
        public static void DeleteSession()
        {
            _session = null;
        }
    }
}
