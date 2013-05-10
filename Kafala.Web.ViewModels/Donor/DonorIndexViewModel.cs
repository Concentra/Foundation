using System;
using System.Collections.Generic;
using Kafala.Entities;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorIndexViewModel
    {
        public virtual IList<DonorViewModel> Donors { get; set; }
    }
}
