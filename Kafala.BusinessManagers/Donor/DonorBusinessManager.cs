using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.BL;
using Foundation.Web;
using Kafala.Entities;
using Foundation.Infrastructure;
using NHibernate;

namespace Kafala.BusinessManagers.Donor
{
   public class DonorBusinessManager : IBusinessManager
    {
       public IFlashMessenger FlashMessenger { get; set; }
       private readonly ISession session;

       public DonorBusinessManager(ISession session, IFlashMessenger flashMessenger)
       {
           FlashMessenger = flashMessenger;
           this.session = session;
       }

       public virtual Guid Add(IDonorContract donor)
       {
           var referral = session.Get<Entities.Donor>(donor.ReferralId);
           var donorObject = new Entities.Donor()
           {
               Id = new Guid(),
               JoinDate = donor.JoinDate ,
               DonorStatus = donor.DonorStatus,
               Name = donor.Name,
               Email = donor.Email,
               Mobile = donor.Mobile,
               Telephone = donor.Telephone,
               Referral = referral
           };

           session.Save(donorObject);
           this.FlashMessenger.AddMessageByKey("CreateDonorSuccess" , FlashMessageType.Success);
           return donorObject.Id;
       }

       public virtual Guid Update(Guid id, IDonorContract donorValue)
       {
           var donor = session.Get<Entities.Donor>(id);
           var referral = session.Get<Entities.Donor>(donorValue.ReferralId);
           if (donor != null)
           {
               donor.JoinDate = donorValue.JoinDate;
               donor.DonorStatus = donorValue.DonorStatus;
               donor.Name = donorValue.Name;
               donor.Email = donorValue.Email;
               donor.Mobile = donorValue.Mobile;
               donor.Telephone = donorValue.Telephone;
               donor.Referral = referral;
           }
           FlashMessenger.AddMessageByKey("UpdateDonorSuccess", FlashMessageType.Success);
           session.Save(donor);
           return id;
       }

       public bool Delete(Guid donorId)
       {
           
           var donor = session.Get<Entities.Donor>(donorId);
           if (CanDelete(donor))
           {
               session.Delete(donor);
               return true;
           }
           else
           {
               return false;
           }
       }

       private bool CanDelete(Entities.Donor donor)
       {
           return true;
       }
    }
}
