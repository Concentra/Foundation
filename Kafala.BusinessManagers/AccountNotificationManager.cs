using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.Notifications;
using StructureMap;

namespace Kafala.BusinessManagers
{
    public class AccountNotificationManager
    {
        public void SendPasswordReminderEmail(string emailAddress)
        {
            var emailService = ObjectFactory.GetInstance<IEmailService>();
            var notificationHelper = new NotificationHelper(emailService);
            notificationHelper.SendEmailWithTemplatePath(emailAddress, string.Empty, "kafala@concentra.co.uk", "Password Reminder", "C:/temp/template.txt", new { name = "Tim", Surname = "Ozuns", newPassword = "pass@word1" });
        }
    }
}
