using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Foundation.Infrastructure.Notifications
{
    public class EmailMessageSender : IEmailMessageSender
    {
        private readonly IEmailLogger emailLogger;

        public EmailMessageSender(IEmailLogger emailLogger)
        {
            this.emailLogger = emailLogger;
        }

        public void Send(string toAddresses, string ccAddresses, string subject, string body)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.To.Add(toAddresses);
                if (ccAddresses != null)
                    mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                mailMessage.BodyEncoding = Encoding.UTF8;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Send(mailMessage);
                }

                emailLogger.LogEmail(mailMessage);
            }
        }
    }
}
