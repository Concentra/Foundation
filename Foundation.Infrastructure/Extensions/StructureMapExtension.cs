using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure.Extensions
{
    public static class StructureMapExtension 
    {
        public static void For<T>(this Registry cfg, Type pluginType, bool singleTone = false)
        {
            if (singleTone)
            {
                cfg.For<T>().Singleton().Use(GetPlugin<T>(pluginType));
            }
            else
            {
                cfg.For<T>().Use(GetPlugin<T>(pluginType));
            }
        }

        private static T GetPlugin<T>(Type pluginType)
        {
            return (T)ObjectFactory.GetInstance(pluginType);
        }

    }
}