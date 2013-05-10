using System;
using System.Security.Cryptography;
using System.Text;

namespace Kafala.BusinessManagers.Security
{
    public class Password
    {
        private const int PasswordSaltLength = 128;

        public Password(string password)
        {
            this.Salt = GenerateSalt();
            this.EncryptedPassword = EncodePassword(password, this.Salt);
        }

        protected Password()
        {
        }

        public string Salt { get; private set; }

        public string EncryptedPassword { get; private set; }

        public static bool CheckPassword(string password, string salt, string encryptedPassword)
        {
            return EncodePassword(password, salt) == encryptedPassword;
        }

        /// <summary>
        /// Generates a random password
        /// </summary>
        /// <returns>a randomly generated password</returns>
        public static string GenerateRandomPassword()
        {
            const int length = 8;
            var specialChars = new[] { '*', '$', '-', '+', '?', '_', '&', '=', '!', '%', '#' };
            var random = new Random();

            var password = new char[length];

            // ensure the password has one upper, one lower, one number
            password[0] = Convert.ToChar(random.Next('A', 'Z' + 1));
            password[1] = Convert.ToChar(random.Next('0', '9' + 1));
            password[2] = Convert.ToChar(random.Next('a', 'z' + 1));

            for (var i = 3; i < password.Length; i++)
            {
                var type = random.Next(0, 4);
                switch (type)
                {
                    case 0:
                        // Insert lower case
                        password[i] = Convert.ToChar(random.Next('a', 'z' + 1));
                        break;
                    case 1:
                        // Insert for upper case
                        password[i] = Convert.ToChar(random.Next('A', 'Z' + 1));
                        break;
                    case 2:
                        // Insert integer value
                        password[i] = Convert.ToChar(random.Next('0', '9' + 1));
                        break;
                    case 3:
                        // Insert special characters
                        password[i] = specialChars[random.Next(0, specialChars.Length)];
                        break;
                }
            }

            return new string(password);
        }

        /// <summary>
        /// Encodes a password.
        /// </summary>
        /// <param name="password">Password to encode.</param>
        /// <param name="salt">Salt to encode password with.</param>
        /// <returns>Encoded password.</returns>
        private static string EncodePassword(string password, string salt)
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
        private static string GenerateSalt()
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
