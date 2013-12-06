﻿using System;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.Reports
{
    public class OverDuePaymentViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid PeriodId { get; set; }

        [EditControl(Order = 1, ElementType = ElementType.Text, Label = "Beneficiary Case Name")]
        public virtual string DonationCaseName { get; set; }

        public virtual Guid DonationCaseId { get; set; }

        [EditControl(Order = 2, ElementType = ElementType.Text, Label = "Donor Name")]
        public virtual string DonorName { get; set; }

        public virtual Guid DonorId { get; set; }

        [EditControl(ElementType = ElementType.WholeNumber)]
        public virtual decimal PaidAmount { get; set; }

        [EditControl(ElementType = ElementType.WholeNumber)]
        public virtual decimal CommittedAmount { get; set; }


        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? LastReminderSentOn { get; set; }


        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? DueDate { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        public virtual string PaymentPeriod { get; set; }

    }
}
