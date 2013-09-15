using System;
using System.Collections.Generic;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.PaymentPeriod
{
    public class ListPaymentPeriodViewModel
    {
        public virtual IEnumerable<ViewPaymentPeriodViewModel> PaymentPeriods { get; set; }
    }
}
