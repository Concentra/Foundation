using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Query.Commitment;
using Kafala.Web.ViewModels.Commitment;
using Kafala.Web.ViewModels.Commitment.Partials;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;

namespace Kafala.Query.DonationCase
{
    public class ViewDonationCaseViewModelPopulator : IQuery<Guid, ViewDonationCaseViewModel>
    {
        private readonly ISession session;
        private readonly IQueryContainer queryContainer;

        public ViewDonationCaseViewModelPopulator(ISession session, IQueryContainer queryContainer)
        {
            this.session = session;
            this.queryContainer = queryContainer;
        }

        public ViewDonationCaseViewModel Execute(Guid id)
        {
            var persistedValue = session.Get<Entities.DonationCase>(id);
         
            var model = Mapper.Map<Entities.DonationCase, ViewDonationCaseViewModel>(persistedValue);

            var commitmentsModel = this.queryContainer.Get<CommitmentListModelPopulator>()
                .Execute(new FilterCommitmentViewModel()
                {
                    DonationCaseId = id
                });

            model.Commitments = commitmentsModel.Commitments;

            return model;
        }
    }
}
