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
    public class UpdateDonorViewModelPopulator : IQuery<Guid, DonorUpdateViewModel>
    {
         private readonly ISession session;

         public UpdateDonorViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public DonorUpdateViewModel Execute(Guid id)
        {
            var donor = this.session.Get<Entities.Donor>(id);

            Mapper.CreateMap<Entities.Donor, DonorUpdateViewModel>();

            var model = Mapper.Map<DonorUpdateViewModel>(donor);
            return model;
        }
    }
}
