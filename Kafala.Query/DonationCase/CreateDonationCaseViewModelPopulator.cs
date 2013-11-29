using Foundation.Infrastructure.Query;
using Kafala.Entities.Enums;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;

namespace Kafala.Query.DonationCase
{
    public class CreateDonationCaseViewModelPopulator : IQuery<string, CreateDonationCaseViewModel>
    {
        private readonly ISession session;

        public CreateDonationCaseViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreateDonationCaseViewModel Execute(string id = "")
        {
            var model = new CreateDonationCaseViewModel
                            {
                                
                                DonationCaseStatus = DonationCaseStatus.Active
                            };
            return model;
        }
    }
}
