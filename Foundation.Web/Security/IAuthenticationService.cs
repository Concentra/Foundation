using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Persistence;

namespace Foundation.Web.Security
{
    public interface IAuthenticationService
    {
        IUserToken GetUser(string userName);
        void RegisterFailedLoginAttempt(IUserToken userToken, int maximumLoginAttempts);
        void ResetFailedLoginAttempts(IUserToken userToken);
        SignInResult SignIn(string userName, string password, bool rememberMe = false);
        void SignOut();
        int PasswordExpiryDays { get; set; }
        int MaximumPasswordAttemptsLimit { get; set; }
    }
}
