using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.Payment;
using NHibernate;

namespace Kafala.Query.Payment
{
    public class PaymentViewModelPopulator : IQuery<Guid, ViewPaymentViewModel>
    {
         private readonly ISession session;

         public PaymentViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public ViewPaymentViewModel Execute(Guid id)
        {
            var source = this.session.Get<Entities.Payment>(id);

            Mapper.CreateMap<Entities.Payment, ViewPaymentViewModel>();

            var model = Mapper.Map<ViewPaymentViewModel>(source);
            return model;
        }
    }
}
