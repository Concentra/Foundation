using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Foundation.Infrastructure.Notifications
{
    public class EmailService : IEmailService
    {
        public void Send(string toAddresses, string ccAddresses, string from, string subject, string body)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.To.Add(toAddresses);
                mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                mailMessage.BodyEncoding = Encoding.UTF8;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Send(mailMessage);
                }

                // this.LogEmailSent(mailMessage, commandName, objectId, alertType);
            }
        }

        //private void LogEmailSent(MailMessage message, string commandName, Guid? objectId, string alertType)
        //{
        //    var logger = new EmailServiceLogger(new ConnectionString("RM"));
        //    logger.Log(Guid.NewGuid(), message.To[0].Address, message.Subject, message.Body, DateTime.Now, commandName, objectId, alertType);
        //}
    }
}
