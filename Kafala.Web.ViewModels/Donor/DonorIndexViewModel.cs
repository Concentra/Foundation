using System;
using System.Collections.Generic;
using Foundation.Web.Paging;
using Kafala.Web.ViewModels.Donor.Partial;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorIndexViewModel : PagedViewModel
    {
        public virtual DonorFilterViewModel DonorFilter { get; set; }
        
        public virtual List<ViewDonorViewModel> Donors { get; set; }
    }
}
