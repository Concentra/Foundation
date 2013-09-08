using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Infrastructure.BL;
using Kafala.Entities;
using Foundation.Infrastructure;
using NHibernate;

namespace Kafala.BusinessManagers.Donor
{
   public class DonorBusinessManager : IBusinessManager
    {
       private readonly ISession session;

       public DonorBusinessManager(ISession session)
       {
           this.session = session;
       }

       public virtual Guid Add(IDonorContract donor)
       {
           var referral = donor.ReferralId != null ? session.Get<Entities.Donor>(donor.ReferralId) : null;
           var donorObject = new Entities.Donor()
           {
               Id = new Guid(),
               JoinDate = joinDate ,
               Referral = referral,
               Name = name,
               Telephone = Mobile,
               
           };

           session.Save(donor);
           return donor.Id;
       }

       public virtual Guid Update(Guid id, string name, string Mobile, DateTime? joinDate, Guid? refferedId)
       {
           var donor = session.Get<Entities.Donor>(id);
           var referral = refferedId.HasValue ? session.Get<Entities.Donor>(refferedId) : null;
           if (donor != null)
           {
               donor.JoinDate = joinDate;
               donor.Referral = referral;
               donor.Name = name;
               donor.Telephone = Mobile;
               session.Save(donor);
           }
           return id;
       }
    }
}
