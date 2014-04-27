using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.Web.ViewModels.Payment;

namespace Kafala.Web.ViewModels.Commitment
{
    public class ViewCommitmentViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid Id { get; set; }

        [Display(Order = 1,  Name = "Beneficiary Case Name")]
        [EditControl(ElementType = ElementType.Text)]
        public virtual string DonationCaseName { get; set; }

        public virtual Guid DonationCaseId { get; set; }


        [Display(Order = 2, Name = "Donor Name")]
        [EditControl(ElementType = ElementType.Text)]
        public virtual string DonorName { get; set; }

        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid DonorId { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? StartDate { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? EndDate { get; set; }

        [EditControl(ElementType = ElementType.FloatingPointNumber)]
        public virtual decimal Amount { get; set; }

        public virtual IList<ViewPaymentViewModel> Payments { get; set; }
    }
}
