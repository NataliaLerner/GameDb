using System.Security.Principal;
using GameDb.DAL.Entities;

namespace GameDb.Base.Authorization
{
    public class СoncreteUserIdentity : IIdentity
    {
        public СoncreteUserIdentity(UserEntity userEntityData)
        {
            _userEntityData = userEntityData;
            IsAuthenticated = _userEntityData != null;
        }

        private UserEntity _userEntityData;
        public UserEntity UserEntityData => _userEntityData;

        public string Name => _userEntityData?.Login;
        public virtual string AuthenticationType => "Custom authentication";

        public bool IsAuthenticated { get; private set; }
    }
}