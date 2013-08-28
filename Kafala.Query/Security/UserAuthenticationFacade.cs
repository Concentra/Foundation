using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Persistence;
using Foundation.Web.Security;
using Kafala.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Security
{
    public class UserAuthenticationFacade : IUserAuthenticationFacade
    {
        private readonly ISession session;

        public UserAuthenticationFacade(ISession session)
        {
            this.session = session;
        }

        public IAuthenticatableUser GetUser(string userName)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userName);
            return (IAuthenticatableUser)user;
        }

        public void RegisterFailedLoginAttempt(IAuthenticatableUser iuser, int maximumLoginAttempts)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == iuser.EmailAddress);
            if (user != null)
            {
                ++user.FailedLoginAttempts;

                if (user.FailedLoginAttempts >= maximumLoginAttempts)
                {
                    user.AccountLocked = true;
                    session.Save(user);
                }
            }
        }

        public void ResetFailedLoginAttempts(IAuthenticatableUser iuser)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == iuser.EmailAddress);
            if (user != null)
            {
                user.FailedLoginAttempts = 0;
                session.Save(user);
            }
        }
    }
}
