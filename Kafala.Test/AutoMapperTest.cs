using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using Foundation.Web.Sorter;
using Kafala.Entities;
using Foundation.Web.Filter;
using Kafala.Query;
using NUnit.Framework;

namespace Kafala.Test
{
    [TestFixture]
    class AutoMapperTest
    {
        [Test(Description = "AutoMapper")]
        public void AutoMapperConfigurationTest()
        {
            var useless = new ListSourceMapper();
            Mapper.Initialize(AutoMapperConfigurations.Configure);
        }

        
    }
}
