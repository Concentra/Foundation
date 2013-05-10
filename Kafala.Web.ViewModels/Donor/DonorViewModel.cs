using System;
using System.Collections.Generic;
using Kafala.Entities;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorViewModel
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Telephone { get; set; }

        public virtual DateTime JoinDate { get; set; }

        public virtual DonorListViewModel Referral { get; set; }

        public virtual IList<Commitment> Commitments { get; set; }
    }
}
