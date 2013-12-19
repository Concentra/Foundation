using System;
using System.Collections.Generic;

namespace Foundation.Infrastructure
{
    public interface IFoundationConfigurator
    {
        Type ViewModelsAssemblyHookType { get; set; }
        Type BusinessInvocationLogger { get; set; }
        Type EntityTypeHolder { get; set; }
        Type AuthenticationService { get; set; }
        Type EmailLogger { get; set; }
        string ConnectionStringKeyName { get; set; }
        Type ResourceLocator { get; set; }
        Dictionary<string, string> EmailConfigurations { get; set; }
        Type FlashMessenger { get; set; }
    }
}