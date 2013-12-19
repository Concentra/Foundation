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
            get
            {
                return typeof (ViewModels.DonationCase.ListDonationCaseViewModel);
            }
            set { }
        }

        public Type BusinessInvocationLogger 
        {
            get
            {
                return typeof (IBusinessManagerInvocationLogger);
                
            }
            set {}
        }

        public Type EntityTypeHolder
        {
            get
            {
                return typeof (Entities.Donor);
            }
            set {}
        }

        public Type AuthenticationService
        {
            get
            {
                return typeof(Kafala.Query.Security.AuthenticationService);
            }
            set {}
        }

        public Type EmailLogger
        {
            get
            {
                return typeof(Kafala.Query.Security.AuthenticationService);
            }
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
        public Type FlashMessenger { get; set; }
    }
}