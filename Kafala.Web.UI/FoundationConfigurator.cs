using System;
using System.Collections.Generic;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;

namespace Kafala.Web.UI
{
    public class FoundationConfigurator : IFoundationConfigurator
    {
        public Type ViewModelsAssemblyHookType
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type BusinessInvocationLogger 
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type EntityTypeHolder
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type AuthenticationService
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type EmailLogger
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ConnectionStringKeyName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Type ResourceLocator
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Dictionary<string, string> EmailConfigurations { get; set; }
    }
}