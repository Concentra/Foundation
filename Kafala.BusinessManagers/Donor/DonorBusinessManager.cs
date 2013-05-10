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

       public virtual Entities.Donor AddDonor(string donorName, string donorMobile)
       {
           Console.Write("AddDonor" + donorName + " " + donorMobile);
           return null;
       }

       public virtual Entities.Donor UpdateDonor(string donorName, string donorMobile)
       {
           return null;
       }
    }
}
