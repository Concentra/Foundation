using System;
using System.Linq;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Foundation.Web.Extensions;
using Kafala.Entities;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class CommitmentCreateModelPopulator : IQuery<CreateCommitmentParameters, CreateCommitmentViewModel>
    {
        private readonly ISession session;

        public CommitmentCreateModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CreateCommitmentViewModel Execute(CreateCommitmentParameters parameters)
        {
            var model = new CreateCommitmentViewModel()
                {
                    DonationCases = session.CreateDropDownList<Entities.DonationCase>(x => x.Name, y => y.Id, parameters.DonationCaseId),
                    Donors = session.CreateDropDownList<Entities.Donor>(x => x.Name, y => y.Id, parameters.DonorId),
                    DonorId = parameters.DonorId,
                    DonationCaseId = parameters.DonationCaseId
                };
            return model;
        }
    }

    public class CreateCommitmentParameters
    {
        public Guid DonationCaseId { get;  set; }
        public Guid DonorId { get;  set; }
    }
}
