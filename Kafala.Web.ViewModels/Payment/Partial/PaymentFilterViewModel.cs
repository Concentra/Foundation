using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Payment.Partial
{
    public class PaymentFilterViewModel : PagingInfoViewModel
    {
        [FilterControl(DataElement = "Commitment.Donor.Name", OperatorOption = Operator.Like)]
        [EditControl(ElementType = ElementType.Text)]
        public string DonorName { get; set; }

        [FilterControl(DataElement = "PaymentPeriod.Id", OperatorOption = Operator.Equal)]
        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "PaymentPeriods", SelectPromptLabel = "Select Payments")]
        public Guid? PaymentPeriod { get; set; }

        public IEnumerable<SelectListItem> PaymentPeriods { get; set; }
        public Guid? DonorId { get; set; }
    }
}