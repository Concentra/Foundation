using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Entities.Enums;
using Kafala.Web.ViewModels.Donor;
using Foundation.Infrastructure;
using NHibernate;
using NHibernate.Linq;
using IQuery = Foundation.Infrastructure.Query.IQuery;
using Foundation.Web.Extensions;

namespace Kafala.Query.Donor
{
    public class DonorCreateModelPopulator : IQuery<string, CreateDonorViewModel>
    {
        private readonly ISession session;

        public DonorCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreateDonorViewModel Execute(string id)
        {
            var model = new CreateDonorViewModel()
                {
                    Name = "Abdo",
                    ListProperty = typeof(DonorStatus).ToSelectListWithNames(),
                    DonorStatus = DonorStatus.Suspended,
                    SelectedItem = DonorStatus.Suspended
                };
            return model;
        }
    }
}
