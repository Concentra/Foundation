using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.DonationCase;
using NHibernate;

namespace Kafala.Query.DonationCase
{
    public class EditDonationCaseViewModelPopulator : IQuery<Guid, EditDonationCaseViewModel>
    {
        private readonly ISession session;

        public EditDonationCaseViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public EditDonationCaseViewModel Execute(Guid id)
        {
            var persistedValue = session.Get<Entities.DonationCase>(id);
            
            var model = Mapper.Map<Entities.DonationCase, EditDonationCaseViewModel>(persistedValue);
            return model;
        }
    }
}
