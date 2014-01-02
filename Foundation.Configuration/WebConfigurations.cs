using System;
using System.Resources;

namespace Foundation.Configuration
{
    public class WebConfigurations : IResourcesLocator
    {
        /// <summary>
        /// a pointer to the assembly where the view models exists. All types in this assembly will be considered as view models.
        /// </summary>
        public Type ViewModelsAssemblyHookType { get; set; }
        
        /// <summary>
        /// a pointer to the assembly hosting your MVC controllers.
        public Type ControllersAssemblyHookType { get; set; }
        
        /// <summary>
        /// You authentication service. A type that implements Foundation.Web.Security.IAuthenticationService
        /// </summary>
        public Type AuthenticationService { get; set; }
        
        /// <summary>
        /// The flash messenger to be used in communicating messages between the business layer and User interface.
        ///  A type that implements Foundation.Infrastructure.Notifications.IFlashMessenger
        /// </summary>
        public Type FlashMessenger { get; set; }
        
        /// <summary>
        /// the resrouce manage for the flash messages
        /// </summary>
        public ResourceManager FlashMessagesResourceManager { get;  set; }
        
        /// <summary>
        /// Resource manager for page titles. the key in the resource file should be the name of the view model omitting the suffix "ViewModel" if it exists.
        /// </summary>
        public ResourceManager PageTitleResourceManager { get;  set; }

        /// <summary>
        /// the default page title if nothing specific is provided in PageTitleResourceManager.
        /// </summary>
        public string DefaultPageTitle { get; set; }
        
        /// <summary>
        /// Resource manager for help content. the key in the resource file should follow the following format ViewModelTypeName_ElementName. 
        /// </summary>
        public ResourceManager HelpResourceManager { get;  set; }
        
        /// <summary>
        /// Configurations for the paging features. 
        /// </summary>
        public PagingConfigurations PagingConfigurations { get; set; }
    }
}