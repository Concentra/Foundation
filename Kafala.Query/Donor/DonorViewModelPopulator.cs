using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.Donor;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Donor
{
    public class DonorViewModelPopulator : IQuery<Guid, ViewDonorViewModel>
    {
         private readonly ISession session;

         public DonorViewModelPopulator(ISession session)
        {
            this.session = session;
        }

        public ViewDonorViewModel Execute(Guid donorId)
        {
            var donor = this.session.Get<Entities.Donor>(donorId);

            Mapper.CreateMap<Entities.Donor, ViewDonorViewModel>();
            var model = new ViewDonorViewModel();

            model = Mapper.Map<ViewDonorViewModel>(donor);
            return model;
        }
    }
}
