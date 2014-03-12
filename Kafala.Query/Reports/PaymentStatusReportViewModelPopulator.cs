using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Web.Extensions;
using Foundation.Web.Paging;
using Foundation.Web.Filter;
using Kafala.Entities;
using Kafala.Query.Donor;
using Kafala.Web.ViewModels.Reports;
using Kafala.Web.ViewModels.Reports.Partials;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping;

namespace Kafala.Query.Reports
{
    public class PaymentStatusReportViewModelPopulator : IQuery<FilterPaymentStatus, PaymentStatusReportViewModel>
    {
        private readonly ISession session;

        public PaymentStatusReportViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public PaymentStatusReportViewModel Execute(FilterPaymentStatus filter)
        {
            var query = this.session.Query<PaymentStatus>()
                .Fetch(x => x.DonationCase)
                .Fetch(x => x.Commitment)
                .ThenFetch(x => x.Donor)
                .Fetch(x => x.Payment)
                .Fetch(x => x.PaymentPeriod).AsQueryable();
            
            query = query.ApplyFilter(filter);

            if (filter.PointInTime.HasValue)
            {
                query = query
                     .Where(x => x.PaymentPeriod.Month == filter.PointInTime.Value.Month
                                         && x.PaymentPeriod.Year == filter.PointInTime.Value.Year);
            }

            var collectedAmountValue = query.Where(x => x.Payment != null)
                .ToFutureValue<PaymentStatus,decimal?>(x => x.Sum(p => (decimal?) p.PaidAmount));

            var collectedAmount = (collectedAmountValue != null && collectedAmountValue.Value != null) ?
                (decimal) collectedAmountValue.Value : 0;

            var expectedAmountValue = query
                 .ToFutureValue<PaymentStatus,decimal?>(x => x.Sum(p => (decimal?) p.CommittedAmount));
               
            var expectedAmount = (expectedAmountValue != null && expectedAmountValue.Value != null) ?
                (decimal)expectedAmountValue.Value : 0;
           
                var pagedCommitments = query.FetchPaged(filter);

            var payments = pagedCommitments.Select(x => new OverDuePaymentViewModel()
                {
                    PaidAmount = x.PaidAmount,
                    DonationCaseId = x.DonationCaseId,
                    DonationCaseName = x.DonationCase != null ? x.DonationCase.Name : string.Empty,
                    PaymentPeriod = x.PaymentPeriod != null ? x.PaymentPeriod.Name : string.Empty,
                    CommittedAmount = x.CommittedAmount,
                    DonorName = x.Commitment != null ? x.Commitment.Donor.Name : string.Empty,
                    DueDate = new DateTime(x.PaymentPeriod.Year, x.PaymentPeriod.Month, 1),
                    DonorId = x.Commitment != null ? x.Commitment.Donor.Id : Guid.Empty,
                    CommitmentId = x.Commitment != null ? x.Commitment.Id : Guid.Empty,
                }).ToList();

                var model = new PaymentStatusReportViewModel()
                {
                    CollectedAmount = collectedAmount,
                    ExpectedAmount = expectedAmount,
                    OutStandingPayments = payments,
                    OverDueAmount = expectedAmount - collectedAmount,
                    FilterPaymentStatus = new FilterPaymentStatus()
                    {
                       DonorList = session.Query<Entities.Donor>().CreateDropDownList(x => x.Name, y => y.Id),
                       PeriodList = session.Query<Entities.PaymentPeriod>().CreateDropDownList(x => x.Name, y => y.Id)
                    },
                   
                };

                model.FilterPaymentStatus.FillPagingParameters(pagedCommitments.PagingViewModel);

                return model;
           
        }
    }
}
