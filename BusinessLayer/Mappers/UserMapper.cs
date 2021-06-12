using BusinessLayer.BusinessEntities;
using DataLayer.DataTransferObjects;

namespace BusinessLayer.Mappers
{
    public static class UserMapper
    {
        public static UserDTO CreateUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO
            {
                Names = user.Names,
                LastName = user.Lastname,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                State = StateMapper.CreateStateDAO(user.State)
            };
            
            return userDTO;
        }

        public static User CreateUser(UserDTO userDAO)
        {
            User user = new User
            {               
                Names = userDAO.Names,
                Lastname = userDAO.LastName,
                EmailAddress = userDAO.EmailAddress,
                Password = userDAO.Password,
                State = StateMapper.CreateState(userDAO.State)
            };

            return user;
        }

        public static UserRegistryDTO CreateUserRegistryDTO(User user)
        {
            UserRegistryDTO userRegistryDTO = new UserRegistryDTO
            {               
                Names = user.Names,
                LastName = user.Lastname,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                UserType = (int)UserType.ServiceRequester,
                StateID = user.State.ID

            };

            return userRegistryDTO;
        }
    }
}
