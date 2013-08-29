using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.Donor;
using Foundation.Infrastructure;
using NHibernate;
using NHibernate.Linq;
using IQuery = Foundation.Infrastructure.Query.IQuery;

namespace Kafala.Query.Donor
{
    public class DonorCreateModelPopulator : IQuery<string, DonorCreateViewModel>
    {
        private readonly ISession session;

        public DonorCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public DonorCreateViewModel Execute(string parameters)
        {
            var model = new DonorCreateViewModel() { Name = "Abdo"};
            return model;
        }
    }
}
