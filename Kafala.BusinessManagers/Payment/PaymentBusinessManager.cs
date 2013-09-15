using System;
using Foundation.Infrastructure.BL;
using Foundation.Web;
using Kafala.Entities;
using NHibernate;

namespace Kafala.BusinessManagers.Payment
{
   public class PaymentBusinessManager : IBusinessManager
    {
       public IFlashMessenger FlashMessenger { get; set; }
       private readonly ISession session;

       public PaymentBusinessManager(ISession session, IFlashMessenger flashMessenger)
       {
           FlashMessenger = flashMessenger;
           this.session = session;
       }

       public virtual Guid Register(IPaymentContract value)
       {
           var persistedObject = new Entities.Payment()
               {
                   Id = new Guid(),
                   Amount = value.Amount,
                   Comments = value.Comments,
                   PaymentDate = value.PaymentDate,
                   PaymentPeriod = session.Get<Entities.PaymentPeriod>(value.PaymentPeriodId)
               };

           session.Save(persistedObject);
           this.FlashMessenger.AddMessageByKey("CreatePaymentSuccess" , FlashMessageType.Success);
           return persistedObject.Id;
       }

       public virtual Guid Update(Guid id, IPaymentContract value)
       {
           var persistedObject = session.Get<Entities.Payment>(id);
           if (persistedObject != null)
           {
               persistedObject.PaymentPeriod = session.Get<Entities.PaymentPeriod>(value.PaymentPeriodId);
               persistedObject.Amount = value.Amount;
               persistedObject.PaymentDate = value.PaymentDate;
               persistedObject.Comments = value.Comments;
           }
           
           session.Save(persistedObject);
           return id;
       }

       public bool Delete(Guid id)
       {

           var persistedObject = session.Get<Entities.Payment>(id);
           if (CanDelete(persistedObject))
           {
               session.Delete(persistedObject);
               return true;
           }
           else
           {
               return false;
           }
       }

       private bool CanDelete(Entities.Payment value)
       {
           return true;
       }
    }
}
