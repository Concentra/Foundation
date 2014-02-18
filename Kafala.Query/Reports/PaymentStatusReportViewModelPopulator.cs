using System;
using System.Linq;
using AutoMapper;
using Foundation.Infrastructure.Query;
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
            var query = this.session.Query<PaymentStatus>();

            
            query = query.ApplyFilter(filter);

            if (filter.PointInTime.HasValue)
            {
                query = query.Where(x => x.PaymentPeriod.Month == filter.PointInTime.Value.Month
                                         && x.PaymentPeriod.Year == filter.PointInTime.Value.Year);
            }

            var collectedAmountValue = query
                .Select<PaymentStatus, decimal?>(x => x.PaidAmount).ToFutureValue<decimal?>();
                

            var collectedAmount = collectedAmountValue.Value == null ? 0 : (decimal) collectedAmountValue.Value;

            var expectedAmountValue = query
              .Select<PaymentStatus, decimal?>(x => x.CommittedAmount).ToFutureValue<decimal?>();

            var expectedAmount = expectedAmountValue.Value == null ? 0 : (decimal)expectedAmountValue.Value;

           
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
                    DonorId = x.Commitment != null ? x.Commitment.Donor.Id : Guid.Empty
                }).ToList();

                var model = new PaymentStatusReportViewModel()
                {
                    CollectedAmount = collectedAmount,
                    ExpectedAmount = expectedAmount,
                    OutStanding = payments,
                    OverDueAmount = expectedAmount - collectedAmount
                };

                model.PagingAndSortingParameters.FillPagingParameters(pagedCommitments.PagingViewModel);

                return model;
           
        }
    }
}
