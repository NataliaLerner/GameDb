using System.Security.Cryptography;
using System.Text;

namespace GameDb.Base.Authorization
{
    public static class PasswordManager
    {
        #region Private

        private static string StaticSalt => "hello_world!";

        private static string FromByteToStr(byte[] bytes)
        {
            var str = GetEncoding().GetString(bytes);
            return str;
        }

        private static byte[] FromStringToByte(string strPassword)
        {
            var bytePassword = GetEncoding().GetBytes(strPassword);
            return bytePassword;
        }

        private static Encoding GetEncoding()
        {
            return Encoding.Default;
        }

        #endregion

        /// <summary>
        /// "Соление" пароля и хеширование алгоритмом SHA384 (SHA2)
        /// </summary>
        public static string Encrypt(string password, string dynamicSalt, string staticSalt)
        {
            var combinedPassword = FromStringToByte(password + dynamicSalt + staticSalt);
            var hashed = new SHA384Managed().ComputeHash(combinedPassword);

            return FromByteToStr(hashed);
        }

        public static string GetDynamicSalt(int saltLength = 8)
        {
            var salt = new byte[saltLength];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return FromByteToStr(salt);
        }

        public static string GetStaticSalt()
        {
            return StaticSalt;
        }
    }
}