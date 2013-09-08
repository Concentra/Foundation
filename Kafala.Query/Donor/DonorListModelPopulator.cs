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
    public class DonorListModelPopulator : IQuery<string, DonorIndexViewModel>
    {
        private readonly ISession session;

        public DonorListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public DonorIndexViewModel Execute(string parameters)
        {
            var model = new DonorIndexViewModel
                            {
                                Donors = this.session.Query<Entities.Donor>()
                                    .Select(x => new ViewDonorViewModel()
                                                     {
                                                         Id = x.Id,
                                                         Name = x.Name,
                                                         JoinDate = x.JoinDate,
                                                         DonorStatus = x.DonorStatus
                                                     }).ToList()
                            };
            return model;
        }
    }
}
