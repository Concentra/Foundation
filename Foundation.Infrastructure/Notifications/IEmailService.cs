namespace Foundation.Infrastructure.Notifications
{
    public interface IEmailService
    {
        void SendEmail(string to, string cc, string subject, string body);
        void SendEmailWithTemplateString(string to, string cc, string subject, string templateContents, object templateValues);
        void SendEmailWithTemplatePath(string to, string cc, string subject, string templatePath, object templateValues);
    }
}