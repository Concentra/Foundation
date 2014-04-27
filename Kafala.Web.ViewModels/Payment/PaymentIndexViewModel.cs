using Foundation.Web.Paging;
using System.Collections.Generic;
using Kafala.Web.ViewModels.Payment.Partial;

namespace Kafala.Web.ViewModels.Payment
{
    public class PaymentIndexViewModel
    {
        public virtual PaymentFilterViewModel PaymentFilter { get; set; }

        public virtual List<ViewPaymentViewModel> Payments { get; set; }
    }
}
