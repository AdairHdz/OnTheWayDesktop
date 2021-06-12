namespace DataLayer.DataTransferObjects
{
    public class UserRegistryDTO
    {
        public string ID { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public string StateID { get; set; }

    }
}
