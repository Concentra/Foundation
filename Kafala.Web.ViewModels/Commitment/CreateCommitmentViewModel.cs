using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.Commitment
{
    public class CreateCommitmentViewModel
    {
        [EditControl(Order = 1, ElementType = ElementType.List,Label = "Beneficiary Case Name")]
        [CollectionInfo(ListSourceMember = "DonationCases",SelectPromptLabel = "Please select",SelectPromptValue = null)]
        public virtual Guid DonationCaseId { get; set; }

        public virtual IEnumerator<SelectListItem> DonationCases { get; set; }

        [EditControl(Order = 2,ElementType = ElementType.Text,Label = "Donor")]
        [CollectionInfo(ListSourceMember = "Donors", SelectPromptLabel = "Please select", SelectPromptValue = null)]
        public virtual string DonorId { get; set; }

        public virtual IEnumerator<SelectListItem> Donors { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime StartDate { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime EndDate { get; set; }

        public virtual IList<PaymentsViewModel> Payments { get; set; }
    }
}
