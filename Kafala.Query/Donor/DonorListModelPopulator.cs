using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.Query;
using Foundation.Web.Paging;
using Foundation.Web.Sorter;
using Foundation.Web.Filter;
using Kafala.Entities;
using Kafala.Web.ViewModels.Donor;
using Foundation.Infrastructure;
using Kafala.Web.ViewModels.Donor.Partial;
using NHibernate;
using NHibernate.Linq;
using IQuery = Foundation.Infrastructure.Query.IQuery;

namespace Kafala.Query.Donor
{
    public class DonorListModelPopulator : IQuery<DonorFilterViewModel, DonorIndexViewModel>
    {
        private readonly ISession session;

        public DonorListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public DonorIndexViewModel Execute(DonorFilterViewModel parameters)
        {

            var donorsQuery = this.session.Query<Entities.Donor>();

            donorsQuery = donorsQuery.ApplyOrder(parameters);

            donorsQuery = donorsQuery.ApplyFilter(parameters);

            var pagedDonors = donorsQuery.FetchPaged(parameters);
            var model = new DonorIndexViewModel
            {
                Donors = pagedDonors.Select(x => new ViewDonorViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    JoinDate = x.JoinDate,
                    Telephone = x.Telephone,
                    DonorStatus = x.DonorStatus
                }).ToList(),
                DonorFilter = new DonorFilterViewModel()
            };

            model.DonorFilter.FillSortingParameters(parameters);

            model.DonorFilter.FillPagingParameters(pagedDonors.PagingViewModel);

            return model;
        }
    }

}
