using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Reports
{
    public class PaymentStatusReportViewModel : PagedViewModel
    {
        public decimal ExpectedAmount { get; set; }

        public decimal CollectedAmount { get; set; }

        public decimal OverDueAmount { get; set; }
        
        public IEnumerable<OverDuePaymentViewModel> OutStanding { get; set; }

    }
}
