using GameDb.Base.Interfaces;

namespace GameDb.Base.Authorization
{
    public class IdentityService : IIdentityService
    {
        public IdentityService()
        {
            _identity = new СoncreteUserIdentity(null);
        }

        private СoncreteUserIdentity _identity;
        public СoncreteUserIdentity Identity => _identity;

        public void SignIn(СoncreteUserIdentity identity)
        {
            if (identity == null) return;
            _identity = identity;
        }

        public void SignOut()
        {
            //TODO: очистка прав
            _identity = null;
        }
    }
}