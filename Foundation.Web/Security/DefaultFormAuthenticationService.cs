using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Foundation.Persistence;
using NHibernate;
using NHibernate.Linq;

namespace Foundation.Web.Security
{
    public class DefaultFormAuthenticationService : IFormAuthenticationService
    {
        private readonly ISession session;
        private readonly IPasswordHelper passwordHelper;
        private readonly IUserAuthenticationFacade userAuthenticationFacade;
        readonly int maximumPasswordAttemptsLimit;


        public DefaultFormAuthenticationService(IPasswordHelper passwordHelper, IUserAuthenticationFacade userAuthFacade)
        {
            this.passwordHelper = passwordHelper;
            this.userAuthenticationFacade = userAuthFacade;
            this.maximumPasswordAttemptsLimit = Convert.ToInt32(ConfigurationManager.AppSettings["MaxmimumPasswordAttempts"]);
        }

        public SignInResult SignIn(string userName, string password, bool rememberMe = false)
        {
            var user = this.userAuthenticationFacade.GetUser(userName);

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

            if (CheckPassword(user.Password, password, user.PasswordSalt))
            {
                userAuthenticationFacade.ResetFailedLoginAttempts(user);
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                return SignInResult.Success;
            }
            else
            {
                userAuthenticationFacade.RegisterFailedLoginAttempt(user, maximumPasswordAttemptsLimit);
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
