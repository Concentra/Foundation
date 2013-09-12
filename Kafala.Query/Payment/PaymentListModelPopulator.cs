using System.Linq;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.Payment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Payment
{
    public class PaymentListModelPopulator : IQuery<string, PaymentIndexViewModel>
    {
        private readonly ISession session;

        public PaymentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public PaymentIndexViewModel Execute(string id)
        {
            var model = new PaymentIndexViewModel
                            {
                                Payments = this.session.Query<Entities.Payment>()
                                    .Select(x => new ViewPaymentViewModel()
                                                     {
                                                         DonationCaseName = x.Commitment.DonationCase.Name,
                                                         DonorName = x.Commitment.Donor.Name,
                                                         Id = x.Id,
                                                         PaymentDate = x.PaymentDate,
                                                         PaymentPeriod = x.PaymentPeriod.Month + "-" + x.PaymentPeriod.Year 
                                                     }).ToList()
                            };
            return model;
        }
    }
}
