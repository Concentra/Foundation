using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using Kafala.Entities;
using NHibernate;
using NHibernate.Linq;
using Foundation.Web.Security;

namespace Kafala.BusinessManagers.Security
{
    public class KafalaFormAuthentication : IFormAuthenticationService
    {
        private readonly ISession session;
        readonly int maximumPasswordAttemptsLimit;
           

        public KafalaFormAuthentication(ISession session)
        {
            this.session = session;
            this.maximumPasswordAttemptsLimit = Convert.ToInt32(ConfigurationManager.AppSettings["MaxmimumPasswordAttempts"]);
        }

        public SignInResult SignIn(string userName, string password, bool rememberMe = false)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userName);
            
            if(user == null)
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

            if(CheckPassword(user.Password, password, user.PasswordSalt))
            {
                user.FailedLoginAttempts = 0;
                session.Save(user);
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                return SignInResult.Success;
            }
            else
            {
                this.RegisterFailedLoginAttempt(user);
                return SignInResult.WrongPassword;
            }
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }


        private void RegisterFailedLoginAttempt(User user)
        {
            ++user.FailedLoginAttempts;
            
            if (user.FailedLoginAttempts >= maximumPasswordAttemptsLimit)
            {
                user.AccountLocked = true;
                session.Save(user);
            }

        }

        private static bool CheckPassword(string referencePassword, string password, string passwordSalt = null)
        {
            if (passwordSalt == null)
            {
                return password.Equals(referencePassword);
            }
            else
            {
                return Password.CheckPassword(password, passwordSalt, referencePassword);
            }
        }
    }
}
