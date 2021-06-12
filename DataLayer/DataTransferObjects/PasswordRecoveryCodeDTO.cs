namespace DataLayer.DataTransferObjects
{
    public class PasswordRecoveryCodeDTO
    {
        public string RecoveryCode { get; set; }
        public string  NewPassword { get; set; }
        public string EmailAddress { get; set; }
    }
}
