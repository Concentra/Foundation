using System.Resources;

namespace Foundation.Web
{
    public interface IResourcesLocator
    {
        ResourceManager FlashMessagesResourceManager { get; }
        string PasswordReminderEmailTemplate { get; }
        ResourceManager PageTitleResourceManager { get; }
        ResourceManager HelpResourceManager { get; }
        string DefaultPageTitle { get; set; }
    }
}