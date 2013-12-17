using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Foundation.Infrastructure.BL;
using Foundation.Persistence;
using Foundation.Web.Security;
using Kafala.BusinessManagers.User;
using Kafala.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISession session;
        private readonly IPasswordHelper passwordHelper;
        private readonly IBusinessManagerContainer businessManagerContainer;
        public int MaximumPasswordAttemptsLimit { get; set; }

        public AuthenticationService(ISession session, IPasswordHelper passwordHelper, IBusinessManagerContainer businessManagerContainer)
        {
            this.session = session;
            this.passwordHelper = passwordHelper;
            this.businessManagerContainer = businessManagerContainer;
            this.MaximumPasswordAttemptsLimit = Convert.ToInt32(ConfigurationManager.AppSettings["MaxmimumPasswordAttempts"]);
            this.PasswordExpiryDays = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordExpiryDays"]);
        }

        public int PasswordExpiryDays { get; set; }

        public IUser GetUser(string userName)
        {
            var user = session.Query<User>().FirstOrDefault(x => x.EmailAddress == userName);
            return user;
        }

        public void RegisterFailedLoginAttempt(IUser userToken, int maximumLoginAttempts)
        {
            var userManager = this.businessManagerContainer.Get<UserManager>();
            userManager.RegisterFailedLoginAttempt(userToken, maximumLoginAttempts);
        }

        public void ResetFailedLoginAttempts(IUser userToken)
        {
            var userManager = this.businessManagerContainer.Get<UserManager>();
            userManager.ResetFailedLoginAttempts(userToken);
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

            var validPassword = CheckPassword(user.Password, password, user.PasswordSalt);

            if (validPassword)
            {
                this.ResetFailedLoginAttempts(user);

                return SignInResult.Success;
            }
            else
            {
                this.RegisterFailedLoginAttempt(user, MaximumPasswordAttemptsLimit);
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
