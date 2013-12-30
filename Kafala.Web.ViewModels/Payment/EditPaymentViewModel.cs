using System;
using System.Collections.Generic;
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
        
        [EditControl(Order = 1, ElementType = ElementType.StaticText, Label = "Donor Name")]
        public virtual string CommitmentDonorName { get; set; }

        [EditControl(Order = 2, ElementType = ElementType.StaticText, Label = "Case Name")]
        public virtual string CommitmentDonationCaseName { get; set; }

        [EditControl(Order = 3, ElementType = ElementType.StaticText, Label = "Payment Period")]
        public virtual string PaymentPeriodName { get; set; }

        public virtual IEnumerable<SelectListItem> PaymentPeriods { get; set; }
        [EditControl(Order = 4, ElementType = ElementType.WholeNumber)]
        public decimal Amount { get; set; }

        [EditControl(Order = 5, ElementType = ElementType.TextArea, Label = "Comment")]
        [AllowHtml]
        public virtual string Comments { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }
    }
}
