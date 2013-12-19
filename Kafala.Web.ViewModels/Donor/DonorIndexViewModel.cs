using System;
using System.Collections.Generic;
using Foundation.Web.Paging;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorIndexViewModel : PagedViewModel
    {
        public virtual List<ViewDonorViewModel> Donors { get; set; }
    }
}
