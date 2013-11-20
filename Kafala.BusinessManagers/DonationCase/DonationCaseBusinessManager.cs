using System;
using AutoMapper;
using Foundation.Infrastructure.BL;
using Foundation.Web;
using NHibernate;
using NHibernate.Mapping.ByCode.Impl;

namespace Kafala.BusinessManagers.DonationCase
{
   public class DonationCaseBusinessManager : IBusinessManager
    {
       private IFlashMessenger flashMessenger { get; set; }
       private readonly ISession session;

       public DonationCaseBusinessManager(ISession session, IFlashMessenger flashMessenger)
       {
           this.flashMessenger = flashMessenger;
           this.session = session;
       }

       public virtual Guid Add(IDonationCaseContract value)
       {
           var persistedObject = new Entities.DonationCase
           {
               DonationCaseStatus = value.DonationCaseStatus,
               EndDate = value.EndDate,
               Name = value.Name,
               StartDate = value.StartDate,
               Id = Guid.NewGuid()
           };

           session.Save(persistedObject);
           this.flashMessenger.AddMessageByKey("CreateDonationCaseSuccess" , FlashMessageType.Success);
           return persistedObject.Id;
       }

       public virtual Guid Update(Guid id, IDonationCaseContract value)
       {
           var persistedObject = session.Get<Entities.DonationCase>(id);
           if (persistedObject != null)
           {
               persistedObject = Mapper.Map<IDonationCaseContract, Entities.DonationCase>(value);
           }
           
           session.Save(persistedObject);
           this.flashMessenger.AddMessageByKey("SaveDonationCaseSuccess", FlashMessageType.Success);
           return id;
       }

       public bool Delete(Guid id)
       {

           var persistedObject = session.Get<Entities.DonationCase>(id);
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

       private bool CanDelete(Entities.DonationCase objectToDelete)
       {
           return true;
       }
    }
}
