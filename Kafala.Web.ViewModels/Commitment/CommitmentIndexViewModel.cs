using System;
using System.Collections.Generic;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Commitment
{
    public class CommitmentIndexViewModel
    {
        public virtual Partials.FilterCommitmentViewModel Search { get; set; }

        public virtual List<ViewCommitmentViewModel> Commitments { get; set; }
    }
}
