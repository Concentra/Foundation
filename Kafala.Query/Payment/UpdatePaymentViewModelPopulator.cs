using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.Payment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Payment
{
    public class UpdatePaymentViewModelPopulator : IQuery<Guid, EditPaymentViewModel>
    {
         private readonly ISession session;

         public UpdatePaymentViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public EditPaymentViewModel Execute(Guid id)
        {
            var source = this.session.Get<Entities.Payment>(id);

            Mapper.CreateMap<Entities.Payment, EditPaymentViewModel>();

            var model = Mapper.Map<EditPaymentViewModel>(source);
             
             model.PaymentPeriods = session.Query<PaymentPeriod>()
                                          .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                                          .OrderBy(x => x.Text);
             
            return model;
        }
    }
}
