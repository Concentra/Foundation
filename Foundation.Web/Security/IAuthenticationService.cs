using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Web.Security
{
    public interface IAuthenticationService
    {
        IUser GetUser(string userName);
        SignInResult SignIn(string userName, string password, bool rememberMe = false);
        void SignOut();
        int PasswordExpiryDays { get; set; }
        int MaximumPasswordAttemptsLimit { get; set; }
    }
}
