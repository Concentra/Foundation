using System;
using System.Web.Mvc;
using Foundation.Web;

namespace Kafala.Test
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

        public MvcHtmlString RenderFlashMessages()
        {
            return MvcHtmlString.Create(string.Empty);
        }

        public MvcHtmlString RenderFlashMessagesForType(FlashMessageType messageType)
        {
            return MvcHtmlString.Create(string.Empty);
        }

        public bool HasMessages()
        {
            return true;
        }
    }
}