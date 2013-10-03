using System.Net.Mail;

namespace Foundation.Infrastructure.Notifications
{
    public interface IEmailLogger
    {
        void LogEmail(MailMessage message);
    }
}