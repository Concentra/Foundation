using System;
using System.Collections.Generic;
using System.Text;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.Commitment
{
    public class ViewCommitmentViewModel
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
        public virtual DateTime StartDate { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime EndDate { get; set; }

        public virtual IList<PaymentsViewModel> Payments { get; set; }
    }
}
