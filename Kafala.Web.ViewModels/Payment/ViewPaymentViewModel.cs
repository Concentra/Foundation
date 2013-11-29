using System;
using System.Collections.Generic;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.Web.ViewModels.Payment;

namespace Kafala.Web.ViewModels.Payment
{
    public class ViewPaymentViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid Id { get; set; }

        [EditControl(Order = 1, 
            ElementType = ElementType.Text,
            Label = "Beneficiary Case Name")]
        public virtual string CommitmentDonationCaseName { get; set; }

        public virtual Guid DonationCaseId { get; set; }


        [EditControl(Order = 2,
            ElementType = ElementType.Text,
            Label = "Donor Name")]
        public virtual string CommitmentDonorName { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid CommitmentDonorId { get; set; }

        [EditControl(ElementType = ElementType.FloatingPointNumber)]
        public virtual decimal Amount { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        public virtual string PaymentPeriodName { get; set; }

    }
}
