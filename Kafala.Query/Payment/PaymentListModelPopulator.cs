using System;
using System.Linq;
using Foundation.Infrastructure.Query;
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

            var paymentModel = paymentList.Select(x => new ViewPaymentViewModel()
            {
                DonationCaseName = x.Commitment.DonationCase.Name,
                DonorName = x.Commitment.Donor.Name,
                Id = x.Id,
                PaymentDate = x.PaymentDate,
                PaymentPeriod = x.PaymentPeriod.Month + "-" + x.PaymentPeriod.Year
            }).ToList();


                                    
            
            var model = new PaymentIndexViewModel
                            {
                                Payments = paymentModel.ToList()
                            };
            return model;
        }
    }

    public class PaymentListParameters
    {
        private readonly Guid? donorId;
        private readonly Guid? caseId;
        private readonly Guid? periodId;

        public PaymentListParameters(Guid? donorId = null, Guid? caseId = null, Guid? periodId = null)
        {
            this.donorId = donorId;
            this.caseId = caseId;
            this.periodId = periodId;
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
