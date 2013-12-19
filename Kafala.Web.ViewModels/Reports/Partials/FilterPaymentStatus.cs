using System;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Reports.Partials
{
    public class FilterPaymentStatus :  PagedViewModel
    {
        [FilterControl(DataElement = "Commitment.Donor.Id")]
        public Guid DonorId { get; set; }

        [FilterControl(DataElement = "PaymentPeriod.Id")]
        public Guid PaymentPeriodId { get; set; }

        [FilterControl(DataElement = "PaymentPeriod.Year")]
        public int PaymentPeriodYear { get; set; }

        public DateTime? PointInTime { get; set; }
    }
}
