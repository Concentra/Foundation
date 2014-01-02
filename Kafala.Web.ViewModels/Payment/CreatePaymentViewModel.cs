using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Payment;

namespace Kafala.Web.ViewModels.Payment
{
    public class CreatePaymentViewModel : IPaymentContract
    {
        [Display(Order = 1)]
        [EditControl(ElementType = ElementType.StaticText)]
        public virtual string DonorName { get; set; }

        [Display(Order = 2)]
        [EditControl(ElementType = ElementType.StaticText)]
        public virtual string DonationCaseName { get; set; }
        
        [Display(Order = 3, Name = "Payment Period")]
        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "PaymentPeriods", SelectPromptLabel = "Please select", SelectPromptValue = null)]
        [Required(ErrorMessage = "Please select a payment period.")]
        public virtual Guid PaymentPeriodId { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        [Required(ErrorMessage = "Please select a commitment.")]
        public Guid CommitmentId { get; set; }

        public virtual IEnumerable<SelectListItem> PaymentPeriods { get; set; }

        [Display(Order = 4)]
        [EditControl(ElementType = ElementType.WholeNumber)]
        [Required(ErrorMessage = "Please select a valid amount.")]
        public decimal Amount { get; set; }

        [Display(Order = 5)]
        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }

        [Display(Order = 6, Name = "Comment")]
        [EditControl(ElementType = ElementType.TextArea)]
        public virtual string Comments { get; set; }

    }
}
