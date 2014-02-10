using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Infrastructure.Notifications
{
    ///<remarks>Please use IEmailService instead to get benefit of Loggin.</remarks>
    ///  <summary>
    /// Please use IEmailService instead to get benefit of Loggin.
    /// </summary>
    public interface IEmailMessageSender
    {
        void Send(string to, string cc, string subject, string body);
    }
}
