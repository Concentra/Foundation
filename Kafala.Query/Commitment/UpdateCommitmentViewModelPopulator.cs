using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Entities;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class UpdateCommitmentViewModelPopulator : IQuery<Guid, EditCommitmentViewModel>
    {
         private readonly ISession session;

         public UpdateCommitmentViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public EditCommitmentViewModel Execute(Guid id)
        {
            var commitment = this.session.Get<Entities.Commitment>(id);

        
            var model = Mapper.Map<EditCommitmentViewModel>(commitment);
             
             model.DonationCases = session.Query<Entities.DonationCase>()
                                          .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                                          .OrderBy(x => x.Text);
             model.Donors = session.Query<Entities.Donor>()
                                   .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                                       .OrderBy(x => x.Text); 

            return model;
        }
    }
}
