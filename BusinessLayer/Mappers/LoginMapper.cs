using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;

namespace BusinessLayer.Mappers
{
    public static class LoginMapper
    {
        public static LoginDTO CreateLoginDTO(User user)
        {
            LoginDTO loginDTO = new LoginDTO
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };

            return loginDTO;
        }

        public static User CreateUser(LoginDTO loginDTO)
        {
            User user = new User
            {
                EmailAddress = loginDTO.EmailAddress,
                Password = loginDTO.Password
            };
            return user;
        }
    }
}
