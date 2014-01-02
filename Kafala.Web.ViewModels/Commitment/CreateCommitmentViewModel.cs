using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Commitment;

namespace Kafala.Web.ViewModels.Commitment
{
    public class CreateCommitmentViewModel : ICommitmentContract
    {
        [Display(Order = 1,Name = "Beneficiary Case Name")]
        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "DonationCases",SelectPromptLabel = "Please select",SelectPromptValue = null)]
        public virtual Guid DonationCaseId { get; set; }

        public virtual IEnumerable<SelectListItem> DonationCases { get; set; }

        [Display(Order = 2, Name = "Donor")]
        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "Donors", SelectPromptLabel = "Please select", SelectPromptValue = null)]
        public virtual Guid DonorId { get; set; }

        public virtual IEnumerable<SelectListItem> Donors { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? StartDate { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? EndDate { get; set; }

        [EditControl(ElementType = ElementType.FloatingPointNumber)]
        public virtual decimal Amount { get; set; }
    }
}
