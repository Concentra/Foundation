using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Persistence;

namespace Foundation.Web.Security
{
    public interface IUserAuthenticationFacade
    {
        IAuthenticatableUser GetUser(string userName);
        void RegisterFailedLoginAttempt(IAuthenticatableUser user, int maximumLoginAttempts);
        void ResetFailedLoginAttempts(IAuthenticatableUser user);
    }
}
