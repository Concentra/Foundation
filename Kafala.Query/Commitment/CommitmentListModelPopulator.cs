using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure.Query;
using Foundation.Web.Filter;
using Foundation.Web.Paging;
using Kafala.Query.Donor;
using Kafala.Web.ViewModels.Commitment;
using Kafala.Web.ViewModels.Commitment.Partials;
using NHibernate;
using NHibernate.Engine.Query;
using NHibernate.Linq;

namespace Kafala.Query.Commitment
{
    public class CommitmentListModelPopulator : IQuery<FilterCommitmentViewModel, CommitmentIndexViewModel>
    {
        private readonly ISession session;

        public CommitmentListModelPopulator(ISession session)
        {
            this.session = session;
        }

        public CommitmentIndexViewModel Execute(FilterCommitmentViewModel commitmentsListParameters)
        {
            var query = this.session.Query<Entities.Commitment>();


            query = query.ApplyFilter(commitmentsListParameters);

            var pagedCommitments = query
                .FetchPaged(commitmentsListParameters.PagingInformationViewModel);
            

            var commitmentModels = query.Select(x => new ViewCommitmentViewModel()
                {
                    DonationCaseName = x.DonationCase.Name,
                    DonorName = x.Donor.Name,
                    Id = x.Id,
                    Amount = x.Amount,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();

            var model = new CommitmentIndexViewModel
            {
                Commitments = commitmentModels,
                Search = commitmentsListParameters,
            };

            model.Search.PagingInformationViewModel.FillPagingParameters(pagedCommitments.PagingViewModel);
            model.Search.DonationCases =
                session.Query<Entities.DonationCase>()
                    .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                    .OrderBy(x => x.Text);

            model.Search.Donors = session.Query<Entities.Donor>()
                   .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                   .OrderBy(x => x.Text);
            
            return model;
        }
    }

    public class CommitmentsListParameters : PagingAndSortingParameters
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
