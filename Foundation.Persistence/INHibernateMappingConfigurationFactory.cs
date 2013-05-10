using System;
using FluentNHibernate.Cfg;

namespace Foundation.Persistence
{
    public interface INHibernateMappingConfigurationFactory
    {
        FluentConfiguration BuildMapping(FluentConfiguration configuration, Type type, string nameSpace);
    }
}