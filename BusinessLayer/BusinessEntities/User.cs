using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using System;
using System.Threading.Tasks;
using Utils;
using Utils.CustomExceptions;

namespace BusinessLayer.BusinessEntities
{
    public class User
    {
        public string Names { get; set; }
        public string Lastname { get; set; }     
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        public string VerificationCode { get; set; }
        public string RecoveryCode { get; set; }
        public UserType UserType { get; set; }
        public State State { get; set; }

        public void Register()
        {
            UserRegistryDTO userRegistryDTO = UserMapper.CreateUserRegistryDTO(this);            
            IRestRequest<UserRegistryDTO> request = new RestRequest<UserRegistryDTO>();
            request.Post("register", userRegistryDTO, false);            
        }

        public void Login()
        {            
            LoginDTO loginDTO = LoginMapper.CreateLoginDTO(this);
            var loginRequest = new RestRequest<LoginResponseDTO>();
            var loginResponseDTO = loginRequest.Post("login", loginDTO, false);                
            Session session = Session.GetSession();
            session.ID = loginResponseDTO.ID;
            session.UserID = loginResponseDTO.UserID;
            session.UserType = loginResponseDTO.UserType;
            session.Verified = loginResponseDTO.Verified;
            session.StateID = loginResponseDTO.StateID;
            session.EmailAddress = loginResponseDTO.EmailAddress;
            session.AuthorizationToken = loginResponseDTO.Token;
            session.RefreshToken = loginResponseDTO.RefreshToken;
        }

        public bool VerifyAccount()
        {
            AccountVerificationCodeDTO accountVerificationCodeDTO = new AccountVerificationCodeDTO
            {
                VerificationCode = this.VerificationCode
            };
            var restRequest = new RestRequest<object>();
            return restRequest.Patch($"users/{Session.GetSession().UserID}/verify", accountVerificationCodeDTO);            
        }

        public bool RefreshVerificationCode()
        {
            RefreshVerificationCodeDTO refreshVerificationCodeDTO = new RefreshVerificationCodeDTO
            {
                EmailAddress = Session.GetSession().EmailAddress
            };
            var restRequest = new RestRequest<object>();
            return restRequest.Put($"users/{Session.GetSession().UserID}/verify", refreshVerificationCodeDTO);            
        }

        public bool RestablishPassword()
        {
            PasswordRecoveryCodeDTO passwordRecoveryCodeDTO = new PasswordRecoveryCodeDTO
            {
                RecoveryCode = this.RecoveryCode,
                NewPassword = this.Password,
                EmailAddress = this.EmailAddress                
            };
            var restRequest = new RestRequest<object>();
            return restRequest.Patch($"users/password", passwordRecoveryCodeDTO);
        }

        public bool ResendRecoveryCode()
        {
            EmailAddressDTO emailAddressDTO = new EmailAddressDTO
            {
                EmailAddress = this.EmailAddress
            };
            var restRequest = new RestRequest<object>();
            return restRequest.Put($"users/recoveryCode", emailAddressDTO);            
        }

    }
}
