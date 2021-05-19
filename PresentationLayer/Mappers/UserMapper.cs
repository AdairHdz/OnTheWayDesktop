using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.Mappers
{
    public class UserMapper
    {
        public static User CreateUserEntity(LoginPresentationModel loginPresentationModel)
        {
            User user = new User
            {
                EmailAddress = loginPresentationModel.EmailAddress,
                Password = loginPresentationModel.Password
            };

            return user;
        }
    }
}
