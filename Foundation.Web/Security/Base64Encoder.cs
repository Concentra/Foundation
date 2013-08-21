using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Foundation.Web.Security
{
    public class Base64Encoder : IPasswordEncoder
    {
        private const int PasswordSaltLength = 128;

        /// <summary>
        /// Encodes a password.
        /// </summary>
        /// <param name="password">Password to encode.</param>
        /// <param name="salt">Salt to encode password with.</param>
        /// <returns>Encoded password.</returns>
        public string EncodePassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt ?? string.Empty);

            var hashBytes = new byte[saltBytes.Length + passwordBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, hashBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, hashBytes, saltBytes.Length, passwordBytes.Length);

            using (var hash = HashAlgorithm.Create())
            {
                hash.TransformFinalBlock(hashBytes, 0, hashBytes.Length);
                return Convert.ToBase64String(hash.Hash);
            }
        }

        /// <summary>
        /// Generate a salt for password encoding.
        /// </summary>
        /// <returns>Salt as a string.</returns>
        public string GenerateSalt()
        {
            var buffer = new byte[PasswordSaltLength];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(buffer);
            }

            var salt = Convert.ToBase64String(buffer);
            if (salt.Length > PasswordSaltLength)
            {
                salt = salt.Substring(0, PasswordSaltLength);
            }

            return salt;
        }
    }
}
