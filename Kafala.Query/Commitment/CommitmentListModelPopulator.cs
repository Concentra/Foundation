using System;
using System.Linq;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Web.Paging;
using Kafala.Web.ViewModels.Commitment;
using NHibernate;
using NHibernate.Engine.Query;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class CommitmentListModelPopulator : IQuery<CommitmentsListParameters, CommitmentIndexViewModel>
    {
        private readonly ISession session;

        public CommitmentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CommitmentIndexViewModel Execute(CommitmentsListParameters commitmentsListParameters)
        {
            var commitmentList = this.session.Query<Entities.Commitment>();
               
            
            if (commitmentsListParameters.DonorId.HasValue)
            {
                commitmentList = commitmentList.Where(x => x.Donor.Id == commitmentsListParameters.DonorId.Value);
            }

            if (commitmentsListParameters.CaseId.HasValue)
            {
                commitmentList = commitmentList.Where(x => x.DonationCase.Id == commitmentsListParameters.CaseId.Value);
            }

            var pagedCommitments = commitmentList
                .FetchPaged(commitmentsListParameters.PageNumber, commitmentsListParameters.PageSize);
            var commitmentModels = pagedCommitments.Select(x => new ViewCommitmentViewModel()
                {
                    DonationCaseName = x.DonationCase.Name,
                    DonorName = x.Donor.Name,
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
            
            var model = new CommitmentIndexViewModel
                            {
                                Commitments = commitmentModels,
                                PagingInfo = Mapper.Map<PagingInfoViewModel>(pagedCommitments.PagingInfo)
                                
                            };
            model.PagingInfo.Sort = commitmentsListParameters.Sort;
            return model;
        }
    }

    public class CommitmentsListParameters : PagingParameters
    {
        private readonly Guid? donorId;
        private readonly Guid? caseId;

        public CommitmentsListParameters()
        {
        }

        public CommitmentsListParameters(Guid? donorId = null, Guid? caseId = null, int pageNumber = 1, int pageSize = 10, string sort = "")
        {
            this.donorId = donorId;
            this.caseId = caseId;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Sort = sort;
        }

        public Guid? DonorId
        {
            get { return donorId; }
        }

        public Guid? CaseId
        {
            get { return caseId; }
        }
    }
}
