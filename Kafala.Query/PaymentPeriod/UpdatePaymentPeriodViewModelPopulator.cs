using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.PaymentPeriod;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.PaymentPeriod
{
    public class UpdatePaymentPeriodViewModelPopulator : IQuery<Guid, EditPaymentPeriodViewModel>
    {
         private readonly ISession session;

         public UpdatePaymentPeriodViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public EditPaymentPeriodViewModel Execute(Guid id)
        {
            var source = this.session.Get<Entities.PaymentPeriod>(id);

            var model = Mapper.Map<EditPaymentPeriodViewModel>(source);

             return model;
        }
    }
}
