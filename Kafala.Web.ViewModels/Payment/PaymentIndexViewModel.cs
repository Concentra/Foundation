using Foundation.Web.Paging;
using System.Collections.Generic;

namespace Kafala.Web.ViewModels.Payment
{
    public class PaymentIndexViewModel : PagedViewModel
    {
        public virtual List<ViewPaymentViewModel> Payments { get; set; }
    }
}
