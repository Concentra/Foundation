using System.Linq;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Web.Paging;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.DonationCase
{
    public class ListDonationCaseViewModelPopulator : IQuery<ListDonationCasesParameters, ListDonationCaseViewModel>
    {
        private readonly ISession session;

        public ListDonationCaseViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public ListDonationCaseViewModel Execute(ListDonationCasesParameters parameters)
        {
            var donationCaseStatusPaged = session.Query<Entities.DonationCase>()
                .FetchPaged(parameters);

            
            
            var model = new ListDonationCaseViewModel
                            {
                                DonationCases = donationCaseStatusPaged.Select(x => new ViewDonationCaseViewModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    DonationCaseStatus = x.DonationCaseStatus,
                                    EndDate = x.EndDate,
                                    StartDate = x.StartDate
                                }),
                            };
            model.PagingAndSortingParameters.FillPagingParameters(donationCaseStatusPaged.PagingViewModel);
            model.PagingAndSortingParameters.FillSortingParameters(parameters);
            
            return model;
        }
    }

    public class ListDonationCasesParameters : PagingAndSortingParameters
    {
        public ListDonationCasesParameters()
        {
        }

        public ListDonationCasesParameters(int pageNumber = 1, int pageSize = 10, string sort = "")
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Sort = sort;
        }
    }
}
