using System;
using System.Linq;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Notifications;
using Foundation.Infrastructure.Security;
using Foundation.Persistence;
using Foundation.Web.Security;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.BusinessManagers.User
{
    public class UserManager : IBusinessManager
    {
        private readonly IEmailService emailService;
        private readonly IAuthenticationService authenticationService;
        private readonly IPasswordHelper passwordHelper;
        private readonly ISession session;

        public UserManager(IEmailService emailService, IAuthenticationService authenticationService, IPasswordHelper passwordHelper,ISession session)
        {
            this.emailService = emailService;
            this.authenticationService = authenticationService;
            this.passwordHelper = passwordHelper;
            this.session = session;
        }


        public virtual Guid RegisterUser(IUserContract userValue)
        {
            var userId = Guid.NewGuid();
            var passwordInfo = passwordHelper.GetEncryptedPasswordAndSalt(userValue.Password);
            var user = new Entities.User()
                {
                    EmailAddress = userValue.EmailAddress,
                    Id = userId,
                    Telephone = userValue.Telephone,
                    FirstName = userValue.FirstName,
                    LastName = userValue.LastName,
                    Password = passwordInfo.EncryptedPassword,
                    PasswordExpirtyDate = DateTime.Now.AddDays(authenticationService.PasswordExpiryDays),
                    PasswordSalt = passwordInfo.Salt,


                    AccountLocked = false,
                    Disabled = false,
                    FailedLoginAttempts = 0
                };
            this.session.Save(user);
            return userId;
        }

        public virtual void SendPasswordReminderEmail(string emailAddress)
        {
            var notificationHelper = this.emailService;
            var user = authenticationService.GetUser(emailAddress);

            notificationHelper.SendEmailWithTemplatePath(emailAddress,
                string.Empty,
                "Password Reminder", "C:/temp/template.txt",
                new { name = user.UserName, newPassword = passwordHelper.GenerateRandomPassword() });
        }

        public virtual void RegisterFailedLoginAttempt(IUser userToken, int maximumLoginAttempts)
        {
            var user = session.Query<Entities.User>().FirstOrDefault(x => x.EmailAddress == userToken.EmailAddress);
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

        public virtual void ResetFailedLoginAttempts(IUser userToken)
        {
            var user = session.Query<Entities.User>().FirstOrDefault(x => x.EmailAddress == userToken.EmailAddress);
            if (user != null)
            {
                user.FailedLoginAttempts = 0;
                session.Save(user);
            }
        }
    }
}
