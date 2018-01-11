using GameDb.DAL.Entities;

namespace GameDb.Base.Interfaces
{
    public interface IRegistrationService
    {
        void RegisterNewUser(UserEntity userEntityData, string strPassword);
    }
}