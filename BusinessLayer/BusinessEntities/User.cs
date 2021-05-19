using BusinessLayer.Mappers;
using DataLayer;
using DataLayer.DataTransferObjects;
using RestSharp;
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
        public UserType UserType { get; set; }
        public State State { get; set; }

        public void Register()
        {
            UserRegistryDTO userRegistryDTO = UserMapper.CreateUserRegistryDTO(this);            
            IRestRequest<UserRegistryDTO> request = new RestRequest<UserRegistryDTO>();
            var response = request.Create("register", userRegistryDTO);
            if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new NetworkRequestException(System.Net.HttpStatusCode.Conflict, "La dirección de correo electrónico" +
                    " ingresada ya se encuentra registrada en el sistema");
            }
        }

        public bool Login()
        {
            LoginDTO loginDTO = LoginMapper.CreateLoginDTO(this);            
            IRestRequest<LoginResponseDTO> loginRequest = new RestRequest<LoginResponseDTO>();
            IRestResponse<LoginResponseDTO> response = loginRequest.Create("login", loginDTO);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoginResponseDTO loginResponseDTO = response.Data;
                Session session = Session.GetSession();
                session.UserID = loginResponseDTO.ID;
                session.UserType = loginResponseDTO.UserType;
                session.Verified = loginResponseDTO.Verified;
                session.StateID = loginResponseDTO.StateID;
                session.AuthorizationToken = loginResponseDTO.Token;
                return true;
            }

            return false;            
        }

        

    }
}
