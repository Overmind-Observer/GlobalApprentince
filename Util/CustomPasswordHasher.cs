using System;
using System.Text;

namespace Global_Intern.Util
{
    public class CustomPasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            string hashedPassword = GenerateSHA256Hash(password, salt);
            return hashedPassword;
        }

        public string CreateSalt(int size)
        {
            // Salt is like a key. Loosing a key (salt) you can't access a door (hash).
            
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            // With increased size, complexity increases
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        /// <summary>
        /// This Mehod is access on registration of user. To create unique hash using user given password and developer given securty type
        /// </summary>
        public string GenerateSHA256Hash(string input, string salt)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
