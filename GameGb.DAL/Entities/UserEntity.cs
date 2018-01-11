using System;

namespace GameDb.DAL.Entities
{
    public class UserEntity
    {
        public int UserId;
        public string Login;
        public string FirstName;
        public string LastName;
        public string Email;
        public DateTime DateCreated;
        public int UserGroup;
        public string UserGroupDescr;

        public string Salt;
        public string Password;
    }
}