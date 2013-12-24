using Foundation.Infrastructure.Security;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure.Configurations
{
    public class SecurityRegistery : Registry
    {
        public SecurityRegistery()
        {
            this.For<IPasswordHelper>().Use<PasswordHelper>();
        }
    }
}