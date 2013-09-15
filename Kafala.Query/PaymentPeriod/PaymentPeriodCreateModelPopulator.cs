using System;
using System.Linq;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.PaymentPeriod;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.PaymentPeriod
{
    public class PaymentPeriodCreateModelPopulator : IQuery<string, CreatePaymentPeriodViewModel>
    {
        private readonly ISession session;

        public PaymentPeriodCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreatePaymentPeriodViewModel Execute(string id)
        {
            var model = new CreatePaymentPeriodViewModel();
            
            return model;
        }
    }
}
