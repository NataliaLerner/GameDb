using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using GameDb.DAL.Base;
using GameDb.DAL.Entities;

namespace GameDb.DAL.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public void Create(UserEntity userEntity)
        {
            var param = new DynamicParameters();

            param.Add("login", userEntity.Login);
            param.Add("password", userEntity.Password);
            param.Add("salt", userEntity.Salt);
            param.Add("firstname", userEntity.FirstName);
            param.Add("lastname", userEntity.LastName);
            param.Add("email", userEntity.Email);

            using (var db = new SqlConnection(_connectionString))
            {
                db.Execute("sp_create_user", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(UserEntity userEntity, int execUserId)
        {
            var param = new DynamicParameters();

            param.Add("login", userEntity.Login);
            param.Add("firstname", userEntity.FirstName);
            param.Add("lastname", userEntity.LastName);
            param.Add("email", userEntity.Email);
            param.Add("user_id", userEntity.UserId);
            param.Add("exec_user_id", execUserId);

            using (var db = new SqlConnection(_connectionString))
            {
                db.Execute("update_user", param, commandType: CommandType.StoredProcedure);
            }
        }

        public UserEntity GetHashAndSalt(string login)
        {
            var sql = "sp_get_user_salt_and_hash";
            var param = new DynamicParameters();
            param.Add("login", login);

            using (var db = new SqlConnection(_connectionString))
            {
                var result = db.QueryFirstOrDefault<UserEntity>(sql, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public UserEntity GetUsreById(int userId)
        {
            return GetUsers(userId, null, null, null, null).FirstOrDefault();
        }

        public List<UserEntity> GetUsers(int? userIdFilter, string firstNameFilter, string lastNameFilter, string loginFilter, int? userId)
        {
            var param = new DynamicParameters();

            param.Add("user_id_filter", userIdFilter);
            param.Add("first_name_filter", firstNameFilter);
            param.Add("last_name_filter", lastNameFilter);
            param.Add("login_filter", loginFilter);
            param.Add("user_id", userId);

            using (var db = new SqlConnection(_connectionString))
            {
                var result = db.Query<UserEntity>("sp_get_users", param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public void CreateUser(string login,
                               string passwordHash,
                               string salt,
                               string firstName,
                               string lastName,
                               string email)
        {
            var param = new DynamicParameters();

            param.Add("login", login);
            param.Add("password_hash", passwordHash);
            param.Add("salt", salt);
            param.Add("firstname", firstName);
            param.Add("lastname", lastName);
            param.Add("email", email);

            using (var db = new SqlConnection(_connectionString))
            {
                var result = db.Query<UserEntity>("sp_get_users", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void AuthenticateUser(string login, string password)
        {
            var param = new DynamicParameters();

            param.Add("login", login);
            param.Add("password", password);

            using (var db = new SqlConnection(_connectionString))
            {
                db.Query("sp_authenticate_user", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}