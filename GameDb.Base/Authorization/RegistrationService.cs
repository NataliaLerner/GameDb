using GameDb.Base.Interfaces;
using GameDb.DAL.Entities;
using GameDb.DAL.Repositories;

namespace GameDb.Base.Authorization
{
    public class RegistrationService : IRegistrationService
    {
        public void RegisterNewUser(UserEntity userEntityData, string strPassword)
        {
            var dynamicSalt = PasswordManager.GetDynamicSalt();
            var staticSalt = PasswordManager.GetStaticSalt();
            var hashedPassword = PasswordManager.Encrypt(strPassword, dynamicSalt, staticSalt);

            userEntityData.Password = hashedPassword;
            userEntityData.Salt = dynamicSalt;

            var urep = new UserRepository();
            urep.Create(userEntityData);
        }
    }
}