using System;
using System.Linq;
using Faker.Extensions;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Kafala.BusinessManagers;
using Kafala.BusinessManagers.Commitment;
using Kafala.BusinessManagers.DonationCase;
using Kafala.BusinessManagers.Donor;
using Kafala.Entities.DoNotMap;
using Kafala.Entities.Enums;
using Moq;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using StructureMap;

namespace Kafala.Test
{
    [TestFixture]
    public class MappingViews : BaseTextFixture
    {
       
        [Test(Description = "Query a Aview")]
        public void QueryViews()
        {
            var paymentStatuses = this.Session.Query<Entities.PaymentStatus>().ToList();
            Assert.Greater(paymentStatuses.Count , 0);         
        }
    }
}
