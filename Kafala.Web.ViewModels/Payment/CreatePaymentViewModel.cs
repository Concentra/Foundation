using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Payment;

namespace Kafala.Web.ViewModels.Payment
{
    public class CreatePaymentViewModel : IPaymentContract
    {
        [EditControl(Order = 1, ElementType = ElementType.StaticText)]
        public virtual string DonorName { get; set; }

        [EditControl(Order = 2, ElementType = ElementType.StaticText)]
        public virtual string DonationCaseName { get; set; }
        
        [EditControl(Order = 3, ElementType = ElementType.List, Label = "Payment Period")]
        [CollectionInfo(ListSourceMember = "PaymentPeriods", SelectPromptLabel = "Please select", SelectPromptValue = null)]
        public virtual Guid PaymentPeriodId { get; set; }

        public virtual IEnumerable<SelectListItem> PaymentPeriods { get; set; }

        [EditControl(Order = 4, ElementType = ElementType.WholeNumber)]
        public decimal Amount { get; set; }

        [EditControl(Order = 5, ElementType = ElementType.DateTime)]
        public virtual DateTime? PaymentDate { get; set; }

        [EditControl(Order = 6, ElementType = ElementType.TextArea, Label = "Comment")]
        public virtual string Comments { get; set; }

    }
}
