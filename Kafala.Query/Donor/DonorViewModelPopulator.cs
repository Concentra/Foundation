using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Query.Commitment;
using Kafala.Query.Payment;
using Kafala.Web.ViewModels.Donor;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Donor
{
    public class DonorViewModelPopulator : IQuery<Guid, ViewDonorViewModel>
    {
         private readonly ISession session;
        private readonly IQueryContainer queryContainer;

        public DonorViewModelPopulator(ISession session, IQueryContainer queryContainer)
         {
             this.session = session;
             this.queryContainer = queryContainer;
         }

        public ViewDonorViewModel Execute(Guid donorId)
        {
            var donor = this.session.Get<Entities.Donor>(donorId);
            var model = Mapper.Map<ViewDonorViewModel>(donor);
            model.DonorDashBoard = new DonorDashBoard()
            {
                DonorId = model.Id,
                Commitments = queryContainer.Get<CommitmentListModelPopulator>()
                .Execute(new CommitmentsListParameters(donorId)).Commitments,
                Payments = queryContainer.Get<PaymentListModelPopulator>()
                .Execute(new PaymentListParameters(donorId)).Payments
            };

            return model;
        }
    }
}
