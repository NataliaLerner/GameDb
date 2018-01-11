using GameDb.Base.Authorization;

namespace GameDb.Base.Interfaces
{
    public interface IIdentityService
    {
        СoncreteUserIdentity Identity { get; }

        void SignIn(СoncreteUserIdentity identity);
        void SignOut();
    }
}