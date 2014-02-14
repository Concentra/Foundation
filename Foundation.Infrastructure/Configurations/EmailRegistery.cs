using Foundation.Infrastructure.Notifications;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure.Configurations
{
    public class EmailRegistery : Registry
    {
        public EmailRegistery()
        {
            this.For<IEmailMessageSender>().Use<EmailMessageSender>();
            this.For<IEmailService>().Use<EmailService>();
        }
    }
}