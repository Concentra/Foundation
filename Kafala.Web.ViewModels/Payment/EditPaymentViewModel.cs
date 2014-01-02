using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Payment;

namespace Kafala.Web.ViewModels.Payment
{
    public class EditPaymentViewModel : IPaymentContract
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid Id { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        public Guid PaymentPeriodId { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        public Guid CommitmentId { get; set; }
        
        [Display(Order = 1, Name = "Donor Name")]
        [EditControl(ElementType = ElementType.StaticText)]
        public virtual string CommitmentDonorName { get; set; }

        [Display(Order = 2, Name = "Case Name")]
        [EditControl(ElementType = ElementType.StaticText)]
        public virtual string CommitmentDonationCaseName { get; set; }

        [Display(Order = 3, Name = "Payment Period")]
        [EditControl(ElementType = ElementType.StaticText)]
        public virtual string PaymentPeriodName { get; set; }

        public virtual IEnumerable<SelectListItem> PaymentPeriods { get; set; }
        [Display(Order = 4)]
        [EditControl(ElementType = ElementType.WholeNumber)]
        public decimal Amount { get; set; }

        [Display(Order = 5, Name = "Comment")]
        [EditControl(ElementType = ElementType.TextArea)]
        [AllowHtml]
        public virtual string Comments { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }
    }
}
