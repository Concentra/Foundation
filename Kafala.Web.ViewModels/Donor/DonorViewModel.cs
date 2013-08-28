﻿using System;
using System.Collections.Generic;
using Kafala.Web.ViewModels.Commitment;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorViewModel
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Telephone { get; set; }

        public virtual DateTime JoinDate { get; set; }

        public virtual DonorViewModel Referral { get; set; }

        public virtual IList<CommitmentItemViewModel> Commitments { get; set; }
    }
}
