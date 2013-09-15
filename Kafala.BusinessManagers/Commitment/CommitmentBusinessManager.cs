using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.BL;
using Foundation.Web;
using Kafala.Entities;
using Foundation.Infrastructure;
using NHibernate;

namespace Kafala.BusinessManagers.Commitment
{
   public class CommitmentBusinessManager : IBusinessManager
    {
       public IFlashMessenger FlashMessenger { get; set; }
       private readonly ISession session;

       public CommitmentBusinessManager(ISession session, IFlashMessenger flashMessenger)
       {
           FlashMessenger = flashMessenger;
           this.session = session;
       }

       public virtual Guid Add(ICommitmentContract commitmentValue)
       {
           var commitmentObject = new Entities.Commitment()
               {
                   Id = new Guid(),
                   DonationCase = session.Get<Entities.DonationCase>(commitmentValue.DonationCaseId),
                   Donor = session.Get<Entities.Donor>(commitmentValue.DonorId),
                   EndDate = commitmentValue.EndDate,
                   StartDate = commitmentValue.StartDate
               };

           session.Save(commitmentObject);
           this.FlashMessenger.AddMessageByKey("CreateCommitmentSuccess" , FlashMessageType.Success);
           return commitmentObject.Id;
       }

       public virtual Guid Update(Guid id, ICommitmentContract commitmentValue)
       {
           var commitmentObject = session.Get<Entities.Commitment>(id);
           if (commitmentObject != null)
           {
               commitmentObject.DonationCase = session.Get<Entities.DonationCase>(commitmentValue.DonationCaseId);
               commitmentObject.Donor = session.Get<Entities.Donor>(commitmentValue.DonorId);
               commitmentObject.EndDate = commitmentValue.EndDate;
               commitmentObject.StartDate = commitmentValue.StartDate;
           }
           
           session.Save(commitmentObject);
           return id;
       }

       public bool Delete(Guid commitmentId)
       {

           var commitment = session.Get<Entities.Commitment>(commitmentId);
           if (CanDelete(commitment))
           {
               session.Delete(commitment);
               return true;
           }
           else
           {
               return false;
           }
       }

       private bool CanDelete(Entities.Commitment commitment)
       {
           return true;
       }
    }
}
