using System;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.PaymentPeriod
{
    public class ViewPaymentPeriodViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid Id { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        public virtual string Name { get; set; }

        [EditControl(ElementType = ElementType.WholeNumber)]
        public virtual int Year { get; set; }

        [EditControl(ElementType = ElementType.WholeNumber)]
        public virtual int Month { get; set; }

    }
}
