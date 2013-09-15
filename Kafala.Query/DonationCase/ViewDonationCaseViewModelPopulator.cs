using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;

namespace Kafala.Query.DonationCase
{
    public class ViewDonationCaseViewModelPopulator : IQuery<Guid, ViewDonationCaseViewModel>
    {
        private readonly ISession session;

        public ViewDonationCaseViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public ViewDonationCaseViewModel Execute(Guid id)
        {
            var persistedValue = session.Get<Entities.DonationCase>(id);
            Mapper.CreateMap<Entities.DonationCase, ViewDonationCaseViewModel>();

            var model = Mapper.Map<Entities.DonationCase, ViewDonationCaseViewModel>(persistedValue);
            return model;
        }
    }
}
