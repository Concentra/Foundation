using System.Resources;
using Foundation.Web;

namespace Kafala.Web.UI
{
    class ResourcesLocator : IResourcesLocator
    {
        public ResourceManager FlashMessagesResourceManager { 
            get
            {
                return Kafala.Web.UI.Resources.KafalaFlashMessages.ResourceManager;
            }
        }

        public string PasswordReminderEmailTemplate
        {
            get; 
            private set;
        }
    }
}