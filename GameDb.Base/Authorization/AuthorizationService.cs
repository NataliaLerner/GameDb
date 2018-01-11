using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using GameDb.Base.Interfaces;
using GameDb.DAL.Entities;
using GameDb.DAL.Repositories;

namespace GameDb.Base.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public СoncreteUserIdentity AuthenticateUser(string login, string password)
        {
            var rep = new UserRepository();
            var passwordWithSalt = rep.GetHashAndSalt(login);

            if (passwordWithSalt == null)
            {
                throw new UnauthorizedAccessException($"Пользователь '{login}' не найден");
            }

            var dynamicSalt = passwordWithSalt.Salt;
            var staticSalt = PasswordManager.GetStaticSalt();

            var rightPassword = passwordWithSalt.Password;
            var testPassword = PasswordManager.Encrypt(password, dynamicSalt, staticSalt);

            if (rightPassword != testPassword)
            {
                throw new UnauthorizedAccessException("Введён неверный пароль");
            }

            var user = rep.GetUsers(null, null, null, login, 0).FirstOrDefault();
            var identity = new СoncreteUserIdentity(user);
            return identity;
        }
    }
}