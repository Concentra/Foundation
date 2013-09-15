using System;
using System.Collections.Generic;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Web.ViewModels.DonationCase
{
    public class ListDonationCaseViewModel
    {
       public virtual IEnumerable<ViewDonationCaseViewModel> DonationCases { get; set; }

    }
}
