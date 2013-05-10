using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kafala.Web.ViewModels.Donor;

namespace Kafala.Entities
{
    public class Commitment
    {
        public virtual Guid Id { get; set; }

        public virtual DonationCase DonationCase { get; set; }

        public virtual DonorViewModel Donor { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual IList<Payment> Payment { get; set; }
    }
}
