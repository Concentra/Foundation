using System;
using System.Collections.Generic;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Commitment
{
    public class CommitmentIndexViewModel : PagedViewModel
    {
        public virtual List<ViewCommitmentViewModel> Commitments { get; set; }
    }
}
