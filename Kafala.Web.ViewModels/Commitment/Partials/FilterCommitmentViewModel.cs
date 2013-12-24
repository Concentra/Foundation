using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Commitment.Partials
{
    public class FilterCommitmentViewModel : PagedViewModel
    {
        [FilterControl(DataElement = "DonationCase.Id")]
        public virtual Guid DonationCaseId { get; set; }

        public virtual IEnumerable<SelectListItem> DonationCases { get; set; }

        [FilterControl(DataElement = "Donor.Id")]
        public virtual Guid DonorId { get; set; }

        public virtual IEnumerable<SelectListItem> Donors { get; set; }

        [FilterControl(DataElement = "StartDate")]
        public virtual DateTime? StartDate { get; set; }

        [FilterControl(DataElement = "EndDate")]
        public virtual DateTime? EndDate { get; set; }
    }
}
