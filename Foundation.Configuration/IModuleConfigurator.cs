using StructureMap;

namespace Foundation.Configuration
{
    public interface IModuleConfigurator
    {
        void Configure(ConfigurationExpression cfg,
                                       IFoundationConfigurator foundationConfigurator);
    }
}