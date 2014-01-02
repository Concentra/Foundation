using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Donor.Partial
{
    public class DonorFilterViewModel 
    {
        [FilterControl(DataElement = "Name", OperatorOption = Operator.Equal)]
        [EditControl(ElementType = ElementType.Text)]
        public string DonorName { get; set; }
    }
}