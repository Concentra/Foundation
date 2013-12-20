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
        Success,
        Failure,
        Warning,
        Information
    }
}