using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Foundation.Web.Extensions;
using Foundation.Web.Filter;
using Foundation.Web.Paging;
using Foundation.Web.Sorter;
using Kafala.Web.ViewModels.Payment;
using Kafala.Web.ViewModels.Payment.Partial;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Payment
{


    public class PaymentListModelPopulator : IQuery<PaymentFilterViewModel, PaymentIndexViewModel>
    {
        private readonly ISession session;

        public PaymentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public PaymentIndexViewModel Execute(PaymentFilterViewModel parameters)
        {
            var paymentList = this.session.Query<Entities.Payment>().ApplyFilter(parameters).ApplyOrder(parameters);

            var pagedPayments = paymentList
                                    .Fetch(x => x.Commitment)
                                        .ThenFetch(x => x.DonationCase)
                                    .Fetch(x => x.Commitment)
                                        .ThenFetch(x => x.Donor)
                                    .Fetch(x => x.PaymentPeriod)
                                    .FetchPaged(parameters);

            
            var paymentModel = pagedPayments.Select(x => new ViewPaymentViewModel()
            {
                CommitmentDonationCaseName = x.Commitment.DonationCase.Name,
                CommitmentDonorName = x.Commitment.Donor.Name,
                Id = x.Id,
                PaymentDate = x.PaymentDate,
                PaymentPeriodName = x.PaymentPeriod.Month + "-" + x.PaymentPeriod.Year
            }).ToList();

            
            var model = new PaymentIndexViewModel
                            {
                                Payments = paymentModel,
                                PaymentFilter = parameters
                            };

            model.PaymentFilter.PaymentPeriods = session.Query<Entities.PaymentPeriod>()
                .CreateDropDownList(x => x.Name, y => y.Id);

            model.PaymentFilter.FillSortingParameters(parameters);

            model.PaymentFilter.FillPagingParameters(pagedPayments.PagingViewModel);

            

            return model;
        }
    }
}
