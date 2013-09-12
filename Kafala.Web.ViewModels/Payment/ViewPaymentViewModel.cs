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
        public virtual string DonationCaseName { get; set; }

        public virtual Guid DonationCaseId { get; set; }


        [EditControl(Order = 2,
            ElementType = ElementType.Text,
            Label = "Donor Name")]
        public virtual string DonorName { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid DonorId { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        public virtual string PaymentPeriod { get; set; }

    }
}
