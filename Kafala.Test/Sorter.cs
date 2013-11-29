using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Foundation.Web.Sorter;
using Kafala.Entities;
using Foundation.Web.Filter;
using NUnit.Framework;

namespace Kafala.Test
{
    [TestFixture]
    class Sorter
    {
        [Test(Description = "Dynamic Sorting")]
        public void CreateDonors()
        {
            var companies = typeof (DonationCase).GetProperties().OrderByDescending(x => x.Name).AsQueryable();


            var orderedQuery = companies.ApplyOrder("Name", "OrderBy");
            var l = 1;
            l = l + 1;
        }

        [Test(Description = "Dynamic Filtering")]
        public void CreateFiltering()
        {
            var companies = typeof(DonationCase).GetProperties().OrderByDescending(x => x.Name).AsQueryable();

            var model = new FilterModel()
            {
                PropertyName = "Size",
                TypeGuid = Guid.NewGuid()
            };

            var filteredElements = companies.ApplyFilter(model);

            var l = 1;
            l = l + 1;
        }
    }
}
