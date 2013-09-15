using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.PaymentPeriod;
using NHibernate;

namespace Kafala.Query.PaymentPeriod
{
    public class PaymentPeriodViewModelPopulator : IQuery<Guid, ViewPaymentPeriodViewModel>
    {
         private readonly ISession session;

         public PaymentPeriodViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public ViewPaymentPeriodViewModel Execute(Guid id)
        {
            var source = this.session.Get<Entities.PaymentPeriod>(id);

            Mapper.CreateMap<Entities.PaymentPeriod, ViewPaymentPeriodViewModel>();

            var model = Mapper.Map<ViewPaymentPeriodViewModel>(source);
            return model;
        }
    }
}
