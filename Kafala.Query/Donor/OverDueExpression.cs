using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security.AntiXss;
using Kafala.Entities;
using Kafala.Entities.Mapping;
using NHibernate.Criterion;

namespace Kafala.Query.Donor
{
    public class OverDueExpression
    {
        public static Func<PaymentStatus, DateTime, bool> OverDue = (p , d) => 
                !p.Paid  && (new DateTime(p.PaymentPeriod.Year , p.PaymentPeriod.Month , 1) > d);
    }
}
