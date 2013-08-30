using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Foundation.Infrastructure.BL;
using Kafala.Entities;
using Foundation.Infrastructure;
using NHibernate;

namespace Kafala.BusinessManagers.Donor
{
    public class DonorInterceptor : BusinessManagerInterceptor<DonorBusinessManager>
    {
       private readonly ISession session;

       public DonorInterceptor(ISession session, IBusinessManagerInvocationLogger businessManagerInvocationLogger) : base(businessManagerInvocationLogger,session)
       {
           this.session = session;
       }

       public override void Intercept(IInvocation invocation)
       {
          invocation.Proceed();
       }
    }
}
