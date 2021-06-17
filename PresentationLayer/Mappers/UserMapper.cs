using BusinessLayer.BusinessEntities;
using PresentationLayer.PresentationModels;

namespace PresentationLayer.Mappers
{
    public static class UserMapper
    {
        public static User CreateUserEntityFromLogin(LoginPresentationModel loginPresentationModel)
        {
            User user = new User
            {
                EmailAddress = loginPresentationModel.EmailAddress,
                Password = loginPresentationModel.Password
            };

            return user;
        }

        public static User CreateUserEntityFromRegistry(RegistryPresentationModel registryPresentationModel)
        {
            User user = new User
            {
                Names = registryPresentationModel.Names,
                Lastname = registryPresentationModel.LastName,
                EmailAddress = registryPresentationModel.EmailAddress,
                Password = registryPresentationModel.Password,
                State = StateMapper.CreateStateEntity(registryPresentationModel.State)
            };
            return user;
        }
    }
}
