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

       public virtual Entities.Donor AddDonor(string name, string Mobile, DateTime? joinDate, Guid? refferedId)
       {
           var referral = refferedId.HasValue ? session.Get<Entities.Donor>(refferedId) : null;
           var donor = new Entities.Donor()
           {
               Id = new Guid(),
               JoinDate = joinDate ,
               Referral = referral,
               Name = name,
               Telephone = Mobile
           };

           session.Save(donor);
           return donor;
       }

       public virtual Entities.Donor UpdateDonor(string donorName, string donorMobile)
       {
           return null;
       }
    }
}
