using GameDb.Base.Authorization;

namespace GameDb.Base.Interfaces
{
    public interface IAuthorizationService
    {
        СoncreteUserIdentity AuthenticateUser(string login, string password);
    }
}