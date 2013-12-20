using System.Web.Mvc;
using Foundation.Infrastructure.Notifications;
using Foundation.Web;

namespace Kafala.Console
{
    internal class SwallowFlashMessneger : IFlashMessenger
    {
        public void AddMessageByKey(string resourceKey, FlashMessageType messageType)
        {
            return;
        }

        public void AddMessage(string message, FlashMessageType messageType)
        {
            return;
        }

        public string RenderFlashMessages()
        {
            return string.Empty;
        }

        public string RenderFlashMessagesForType(FlashMessageType messageType)
        {
            return string.Empty;
        }

        public bool HasMessages()
        {
            return true;
        }
    }
}