using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Foundation.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Foundation.Testing
{
    [TestClass]
    public class DI
    {
        [TestMethod]
        public void assert_bootstrapper_is_valid()
        {
            using (var container = new Container())
            {
                
                
                container.AssertConfigurationIsValid();
            }
        }
    }
}
