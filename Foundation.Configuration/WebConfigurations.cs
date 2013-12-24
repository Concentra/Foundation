using System;
using System.Resources;

namespace Foundation.Configuration
{
    public class WebConfigurations : IResourcesLocator
    {
        public Type ViewModelsAssemblyHookType { get; set; }
        public Type ControllersAssemblyHookType { get; set; }
        public Type AuthenticationService { get; set; }
        public Type FlashMessenger { get; set; }
        public ResourceManager FlashMessagesResourceManager { get;  set; }
        public string PasswordReminderEmailTemplate { get;  set; }
        public ResourceManager PageTitleResourceManager { get;  set; }
        public ResourceManager HelpResourceManager { get;  set; }
        public string DefaultPageTitle { get; set; }
        public PagingConfigurations PagingConfigurations { get; set; }
    }
}