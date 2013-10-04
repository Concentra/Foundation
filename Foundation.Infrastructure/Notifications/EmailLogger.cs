using System.Net.Mail;

namespace Foundation.Infrastructure.Notifications
{
    public class EmailLogger : IEmailLogger
    {
        public void LogEmail(MailMessage message)
        {
            return;
        }
    }
}