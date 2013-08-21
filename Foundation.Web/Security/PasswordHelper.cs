using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Web.Security
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordEncoder encoder;

        public PasswordHelper(IPasswordEncoder encoder)
        {
            this.encoder = encoder;
        }

        public PasswordInfo GetEncryptedPasswordAndSalt(string password)
        {
            var salt = this.encoder.GenerateSalt();
            return new PasswordInfo
            {
                Salt = salt,
                EncryptedPassword = this.encoder.EncodePassword(password, salt)
            };
        }

        public bool CheckPassword(string password, string salt, string encryptedPassword)
        {
            return this.encoder.EncodePassword(password, salt) == encryptedPassword;
        }

        /// <summary>
        /// Generates a random password
        /// </summary>
        /// <returns>a randomly generated password</returns>
        public string GenerateRandomPassword()
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
    }
}
