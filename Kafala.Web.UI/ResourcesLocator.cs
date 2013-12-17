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

        public ResourceManager PageTitleResourceManager
        {
            get
            {
                return null;
            }
        }

        public ResourceManager HelpResourceManager
        {
            get
            {
                return null;
            }
        }

        public string DefaultPageTitle { get; set; }

        public string PasswordReminderEmailTemplate
        {
            get; 
            private set;
        }
    }
}