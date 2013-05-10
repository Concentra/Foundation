using System;
using System.Collections.Generic;
using Kafala.Entities;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorListViewModel
    {
        public virtual List<DonorViewModel> Donors { get; set; }
    }
}
