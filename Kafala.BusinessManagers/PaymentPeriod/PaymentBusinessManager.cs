using System;
using Foundation.Infrastructure.BL;
using Foundation.Web;
using NHibernate;

namespace Kafala.BusinessManagers.PaymentPeriod
{
   public class PaymentPeriodBusinessManager : IBusinessManager
    {
       public IFlashMessenger FlashMessenger { get; set; }
       private readonly ISession session;

       public PaymentPeriodBusinessManager(ISession session, IFlashMessenger flashMessenger)
       {
           FlashMessenger = flashMessenger;
           this.session = session;
       }

       public virtual Guid Add(int year, int month, string name)
       {
           var persistedObject = new Entities.PaymentPeriod()
               {
                   Id = new Guid(),
                   Month = month,
                   Year = year,
                   Name = name
               };

           session.Save(persistedObject);
           this.FlashMessenger.AddMessageByKey("CreatePaymentPeriodSuccess" , FlashMessageType.Success);
           return persistedObject.Id;
       }

       public virtual Guid Update(Guid id, int year, int month, string name)
       {
           var persistedObject = session.Get<Entities.PaymentPeriod>(id);
           if (persistedObject != null)
           {
               persistedObject.Month = month;
               persistedObject.Year = year;
               persistedObject.Name = name;
           }
           
           session.Save(persistedObject);
           return id;
       }

       public bool Delete(Guid id)
       {

           var persistedObject = session.Get<Entities.PaymentPeriod>(id);
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

       private bool CanDelete(Entities.PaymentPeriod value)
       {
           return true;
       }
    }
}
