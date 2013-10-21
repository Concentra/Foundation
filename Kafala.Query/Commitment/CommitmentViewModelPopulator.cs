using System;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;

namespace Kafala.Query.Commitment
{
    public class CommitmentViewModelPopulator : IQuery<Guid, ViewCommitmentViewModel>
    {
         private readonly ISession session;

         public CommitmentViewModelPopulator(ISession session)
        {
            this.session = session;
        }

         public ViewCommitmentViewModel Execute(Guid id)
        {
            var commitment = this.session.Get<Entities.Commitment>(id);

            Mapper.CreateMap<Entities.Commitment, ViewCommitmentViewModel>();
            Mapper.CreateMap<Entities.Payment, PaymentsViewModel>();

            var model = Mapper.Map<ViewCommitmentViewModel>(commitment);
            return model;
        }
    }
}
