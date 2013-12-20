using System.Resources;

namespace Foundation.Configuration
{
    public interface IResourcesLocator
    {
        ResourceManager FlashMessagesResourceManager { get; }
        ResourceManager PageTitleResourceManager { get; }
        ResourceManager HelpResourceManager { get; }
        string DefaultPageTitle { get; set; }
    }
}