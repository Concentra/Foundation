using System;
using System.Collections.Generic;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.DonationCase
{
    public class ListDonationCaseViewModel : PagedViewModel
    {
       public virtual IEnumerable<ViewDonationCaseViewModel> DonationCases { get; set; }

    }
}
