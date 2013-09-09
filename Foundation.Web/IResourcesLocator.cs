using System.Resources;

namespace Foundation.Web
{
    public interface IResourcesLocator
    {
        ResourceManager FlashMessagesResourceManager { get; }
    }
}