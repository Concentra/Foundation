using System.Web.Mvc;

namespace Foundation.Web
{
    public interface IFlashMessenger
    {
        void AddMessageByKey(string resourceKey, FlashMessageType messageType);

        void AddMessage(string message, FlashMessageType messageType);

        string RenderFlashMessages();

        MvcHtmlString RenderFlashMessagesForType(FlashMessageType messageType);

        bool HasMessages();
    }

    public enum FlashMessageType
    {
        Success,
        Failure,
        Warning,
        Information
    }
}