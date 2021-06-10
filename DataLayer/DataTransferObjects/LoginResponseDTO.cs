namespace DataLayer.DataTransferObjects
{
    public class LoginResponseDTO
    {
        public string ID { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int UserType { get; set; }
        public bool Verified { get; set; }
        public string StateID { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
