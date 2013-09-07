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
