using System;
using System.Linq;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.Payment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Payment
{
    public class PaymentCreateModelPopulator : IQuery<string, CreatePaymentViewModel>
    {
        private readonly ISession session;

        public PaymentCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreatePaymentViewModel Execute(string id)
        {
            var model = new CreatePaymentViewModel()
                {
                   PaymentPeriods = 
                   session.Query<Entities.PaymentPeriod>()
                    .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                    .OrderBy(x => x.Text),
                    PaymentDate = DateTime.Now
                };
            return model;
        }
    }
}
