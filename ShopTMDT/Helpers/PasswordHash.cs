using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ShopTMDT.Helpers
{
    public class PasswordHash
    {
        public byte[] GetBytes(int Length)
        {
            var bytes = new byte[Length];
            using(var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }
            return bytes;
        }

        public string GetPassRandom(int length)
        {
            string pass = "";
            string character = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            byte[] bytes = new byte[1];
            char[] chars = new char[length];
            using (var random = RandomNumberGenerator.Create())
            {
                for(int i =0; i< length; i++)
                {
                    random.GetBytes(bytes);
                    chars[i] = character[bytes[0] % (character.Length + 1)];
                }

            }
            pass = new string(chars);
            return pass;
        }

        public string HashPassword(string password)
        {
            byte[] salt = GetBytes(16);
            string passHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 32
                )) ;
            return Convert.ToBase64String(salt) +"|"+ passHash;
        }

        public bool verifyPassword(string password,string passhash)
        {
            string[] parts = passhash.Split("|");
            byte[] salt = Convert.FromBase64String(parts[0]);
            string passwordhash = parts[1];
            var pass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 32
                )) ;
            return pass == passwordhash;
        }
    }
}
