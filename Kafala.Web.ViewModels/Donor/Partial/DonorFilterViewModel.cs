using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Donor.Partial
{
    [Bind(Include = "DonorName,PageSize,PageNumber,Sort,SortDirection")]
    public class DonorFilterViewModel : PagingInfoViewModel
    {
        [FilterControl(DataElement = "Name", OperatorOption = Operator.Like)]
        [EditControl(ElementType = ElementType.Text)]
        public string DonorName { get; set; }
    }
}