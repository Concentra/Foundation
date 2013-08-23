using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Infrastructure.Notifications
{
    public interface IEmailService
    {
        void Send(string to, string cc, string from, string subject, string body);
    }
}
