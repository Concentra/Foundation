using System.Linq;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class CommitmentCreateModelPopulator : IQuery<string, CreateCommitmentViewModel>
    {
        private readonly ISession session;

        public CommitmentCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreateCommitmentViewModel Execute(string id)
        {
            var model = new CreateCommitmentViewModel()
                {
                   DonationCases = 
                   session.Query<DonationCase>()
                    .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                    .OrderBy(x => x.Text),
                   Donors = session.Query<Entities.Donor>()
                   .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                };
            return model;
        }
    }
}
