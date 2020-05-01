using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace restful_API.Controllers
{
    public static class SecurityService
    {
        public static string GetSalt()
        {
            var rngService = new RNGCryptoServiceProvider();

            // Maximum length of salt
            int max_length = 32;
            byte[] salt = new byte[max_length];

            // Build the random bytes
            rngService.GetNonZeroBytes(salt);
            return Convert.ToBase64String(salt);
        }


        public static string GetHash(string data)
        {
            var sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
            //вычисляет хеш от хеша от ...
            for (int i = 0; i < 99; i++)
            {
                hash = sha512.ComputeHash(hash);
            }
            return Convert.ToBase64String(hash);
        }
    }
}
