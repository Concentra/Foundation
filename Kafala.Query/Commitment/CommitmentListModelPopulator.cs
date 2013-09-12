using System.Linq;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class CommitmentListModelPopulator : IQuery<string, CommitmentIndexViewModel>
    {
        private readonly ISession session;

        public CommitmentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CommitmentIndexViewModel Execute(string id)
        {
            var model = new CommitmentIndexViewModel
                            {
                                Commitments = this.session.Query<Entities.Commitment>()
                                    .Select(x => new ViewCommitmentViewModel()
                                                     {
                                                         DonationCaseName = x.DonationCase.Name,
                                                         DonorName = x.Donor.Name,
                                                         Id = x.Id,
                                                         StartDate = x.StartDate,
                                                         EndDate = x.EndDate
                                                     }).ToList()
                            };
            return model;
        }
    }
}
