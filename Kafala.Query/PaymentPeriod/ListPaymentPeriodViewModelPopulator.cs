using System.Linq;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.PaymentPeriod;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.PaymentPeriod
{
    public class ListPaymentPeriodViewModelPopulator : IQuery<string, ListPaymentPeriodViewModel>
    {
        private readonly ISession session;

        public ListPaymentPeriodViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public ListPaymentPeriodViewModel Execute(string dummy = "")
        {
            var paymentList = this.session.Query<Entities.PaymentPeriod>();

            var paymentModel = paymentList.Select(x => new ViewPaymentPeriodViewModel()
            {
                Id = x.Id,
                Month = x.Month,
                Year = x.Year,
                Name = x.Name
            }).ToList();

            var model = new ListPaymentPeriodViewModel()
                            {
                                PaymentPeriods = paymentModel.ToList()
                            };
            return model;
        }
    }
}
