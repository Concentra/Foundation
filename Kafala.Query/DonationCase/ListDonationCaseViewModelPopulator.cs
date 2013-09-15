using System.Linq;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.DonationCase
{
    public class ListDonationCaseViewModelPopulator : IQuery<string, ListDonationCaseViewModel>
    {
        private readonly ISession session;

        public ListDonationCaseViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public ListDonationCaseViewModel Execute(string id)
        {
            var model = new ListDonationCaseViewModel
                            {
                                DonationCases = session.Query<Entities.DonationCase>()
                                .Select(x => new ViewDonationCaseViewModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    DonationCaseStatus = x.DonationCaseStatus,
                                    EndDate = x.EndDate,
                                    StartDate = x.StartDate
                                })
                            };
            return model;
        }
    }
}
