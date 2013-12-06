using System;
using System.Linq;
using Foundation.Infrastructure.Query;
using Foundation.Web.Paging;
using Foundation.Web.Filter;
using Kafala.Entities;
using Kafala.Web.ViewModels.Reports;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Reports
{
    public class PaymentStatusReportViewModelPopulator : IQuery<Guid, PaymentStatusReportViewModel>
    {
        private readonly ISession session;

        public PaymentStatusReportViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public PaymentStatusReportViewModel Execute(Guid id)
        {
            var query = this.session.Query<PaymentStatus>();

            query = query.ApplyFilter(id);

            var pagedCommitments = query.FetchPaged(commitmentsListParameters.PagingInfo);

            var payments = query.Select(x => new OverDuePaymentViewModel()
            {
                PaidAmount = x.PaidAmount,
                DonationCaseId = x.DonationCase.Id,
                DonationCaseName = x.DonationCase.Name,
                PaymentPeriod = x.PaymentPeriod.Name,
                CommittedAmount = x.CommitedAmount,
                DonorName = x.Commitment.Donor.Name,
                DueDate = new DateTime(x.PaymentPeriod.Year, x.PaymentPeriod.Month , 1),
                DonorId = x.Commitment.Donor.Id
            }).ToList();

            return payments;


        }
    }
}
