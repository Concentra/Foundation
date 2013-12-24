using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace Foundation.Web.Configurations
{
    public class ControllerRegistrationConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (typeof(IController).IsAssignableFrom(type) && !type.IsAbstract)
            {
                string name = type.Name.Replace("Controller", string.Empty);
                registry.For<IController>().Add(new ConfiguredInstance(type).Named(name));
            }
        }
    }
}