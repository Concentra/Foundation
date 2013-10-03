using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Web.Security
{
    public interface IPasswordHelper
    {
        PasswordInfo GetEncryptedPasswordAndSalt(string password);
        bool CheckPassword(string password, string salt, string encryptedPassword);
        string GenerateRandomPassword();
        
    }
}
