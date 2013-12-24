namespace Foundation.Infrastructure.Security
{
    public interface IPasswordHelper
    {
        PasswordInfo GetEncryptedPasswordAndSalt(string password);
        bool CheckPassword(string password, string salt, string encryptedPassword);
        string GenerateRandomPassword();
        
    }
}
