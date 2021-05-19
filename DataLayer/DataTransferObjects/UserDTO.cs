namespace DataLayer.DataTransferObjects
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public StateDTO State { get; set; }
    }
}
