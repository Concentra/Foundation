using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Foundation.Web.Extensions;
using Kafala.Entities;
using Kafala.Web.ViewModels.Payment;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Type;
using NHibernate.Util;

namespace Kafala.Query.Payment
{
    public class PaymentCreateModelPopulator : IQuery<Guid, CreatePaymentViewModel>
    {
        private readonly ISession session;

        public PaymentCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreatePaymentViewModel Execute(Guid id)
        {
            var commitment = session.Get<Entities.Commitment>(id);
            var model = new CreatePaymentViewModel()
                {
                    PaymentPeriods = session.Query<Entities.PaymentPeriod>().AsEnumerable().CreateDropDownList(x => x.Name, y => y.Id),
                    PaymentDate = DateTime.Now,
                    DonorName = commitment.Donor.Name,
                    Amount = commitment.Amount,
                    DonationCaseName = commitment.DonationCase.Name,
                    CommitmentId = commitment.Id
                };
            return model;
        }

       
    }
}
