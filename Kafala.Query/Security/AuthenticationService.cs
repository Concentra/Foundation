using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Foundation.Persistence;
using Foundation.Web.Security;
using Kafala.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISession session;
        private readonly IPasswordHelper passwordHelper;
        private readonly int maximumPasswordAttemptsLimit;

        public AuthenticationService(ISession session, IPasswordHelper passwordHelper)
        {
            this.session = session;
            this.passwordHelper = passwordHelper;
            this.maximumPasswordAttemptsLimit = Convert.ToInt32(ConfigurationManager.AppSettings["MaxmimumPasswordAttempts"]);
            this.PasswordExpiryDays = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordExpiryDays"]);
        }

        public int PasswordExpiryDays { get; set; }

        public IUserToken GetUser(string userName)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userName);
            var userObject = (UserToken)((UserToken)(object)user);
            return userObject;
        }

        public void RegisterFailedLoginAttempt(IUserToken userToken, int maximumLoginAttempts)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userToken.EmailAddress);
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

        public void ResetFailedLoginAttempts(IUserToken userToken)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userToken.EmailAddress);
            if (user != null)
            {
                user.FailedLoginAttempts = 0;
                session.Save(user);
            }
        }

        public SignInResult SignIn(string userName, string password, bool rememberMe = false)
        {
            var user = this.GetUser(userName);

            if (user == null)
            {
                return SignInResult.NoSuchUser;
            }

            if (user.Disabled)
            {
                return SignInResult.UserDisabledByAdmin;
            }

            if (user.AccountLocked)
            {
                return SignInResult.UserAlreadyLocked;
            }

            if (user.AccountLocked)
            {
                return SignInResult.LockedForExcessiveLoginAttempts;
            }

            var validPassword = CheckPassword(user.Password, password, user.PasswordSalt);

            if (validPassword)
            {
                this.ResetFailedLoginAttempts(user);
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                return SignInResult.Success;
            }
            else
            {
                this.RegisterFailedLoginAttempt(user, maximumPasswordAttemptsLimit);
                return SignInResult.WrongPassword;
            }
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }


        private bool CheckPassword(string referencePassword, string password, string passwordSalt = null)
        {
            if (passwordSalt == null)
            {
                return password.Equals(referencePassword);
            }
            else
            {
                return passwordHelper.CheckPassword(password, passwordSalt, referencePassword);
            }
        }
    }
}
