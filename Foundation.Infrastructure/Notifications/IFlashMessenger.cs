using System.ComponentModel;

namespace Foundation.Infrastructure.Notifications
{
    public interface IFlashMessenger
    {
        void AddMessageByKey(string resourceKey, FlashMessageType messageType);

        void AddMessage(string message, FlashMessageType messageType);

        string RenderFlashMessages();

        string RenderFlashMessagesForType(FlashMessageType messageType);

        bool HasMessages();
    }

    public enum FlashMessageType
    {
        [Description("success")]
        Success,
        [Description("danger")]
        Failure,
        [Description("warning")]
        Warning,
        [Description("info")]
        Information
    }
}