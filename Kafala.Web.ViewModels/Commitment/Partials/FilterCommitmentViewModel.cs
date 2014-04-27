using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Commitment.Partials
{
    public class FilterCommitmentViewModel : PagingInfoViewModel
    {
        [FilterControl(DataElement = "DonationCase.Id")]
        public virtual Guid DonationCaseId { get; set; }

        public virtual IEnumerable<SelectListItem> DonationCases { get; set; }

        [FilterControl(DataElement = "Donor.Id")]
        public virtual Guid DonorId { get; set; }

        [FilterControl(DataElement = "Donor.Name", OperatorOption = Operator.Like)]
        [EditControl(ElementType = ElementType.Text)]
        public virtual string DonorName { get; set; }

        [FilterControl(DataElement = "DonationCase.Name", OperatorOption = Operator.Like)]
        [EditControl(ElementType = ElementType.Text)]
        public virtual string CaseName { get; set; }

        
        [FilterControl(DataElement = "StartDate")]
        public virtual DateTime? StartDate { get; set; }

        [FilterControl(DataElement = "EndDate")]
        public virtual DateTime? EndDate { get; set; }
    }
}
