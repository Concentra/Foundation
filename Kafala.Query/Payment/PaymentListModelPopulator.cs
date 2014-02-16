using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Foundation.Web.Paging;
using Kafala.Web.ViewModels.Payment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Payment
{


    public class PaymentListModelPopulator : IQuery<PaymentListParameters, PaymentIndexViewModel>
    {
        private readonly ISession session;

        public PaymentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public PaymentIndexViewModel Execute(PaymentListParameters parameters)
        {
            var paymentList = this.session.Query<Entities.Payment>();

            if (parameters.DonorId.HasValue)
            {
                paymentList = paymentList.Where(x => x.Commitment.Donor.Id == parameters.DonorId.Value);
            }

            if (parameters.CaseId.HasValue)
            {
                paymentList = paymentList.Where(x => x.Commitment.Id == parameters.CaseId.Value);
            }

            if (parameters.PeriodId.HasValue)
            {
                paymentList = paymentList.Where(x => x.PaymentPeriod.Id == parameters.PeriodId.Value);
            }

            var pagedPayments = paymentList.FetchPaged(parameters);

            
            var paymentModel = pagedPayments.Select(x => new ViewPaymentViewModel()
            {
                CommitmentDonationCaseName = x.Commitment.DonationCase.Name,
                CommitmentDonorName = x.Commitment.Donor.Name,
                Id = x.Id,
                PaymentDate = x.PaymentDate,
                PaymentPeriodName = x.PaymentPeriod.Month + "-" + x.PaymentPeriod.Year
            }).ToList();

            
            var model = new PaymentIndexViewModel
                            {
                                Payments = paymentModel,
                            };

            model.PagingAndSortingParameters.FillSortingParameters(parameters);

            model.PagingAndSortingParameters.FillPagingParameters(pagedPayments.PagingViewModel);

            return model;
        }
    }

    public class PaymentListParameters : PagingAndSortingParameters
    {
        private readonly Guid? donorId;
        private readonly Guid? caseId;
        private readonly Guid? periodId;
        
        public PaymentListParameters()
        {}
        
        public PaymentListParameters(Guid? donorId = null, Guid? caseId = null, Guid? periodId = null, int pageNumber = 1, int pageSize = 10, string sort = "")
        {
            this.donorId = donorId;
            this.caseId = caseId;
            this.periodId = periodId;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Sort = sort;
        }

        public Guid? DonorId
        {
            get { return donorId; }
        }

        public Guid? CaseId
        {
            get { return caseId; }
        }

        public Guid? PeriodId
        {
            get { return periodId; }
        }
    }
}
