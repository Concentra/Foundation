using System;
using System.Collections.Generic;
using System.Text;

namespace Kafala.Entities
{
    public class Commitment
    {
        public virtual Guid Id { get; set; }

        public virtual DonationCase DonationCase { get; set; }

        public virtual Donor Donor { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual IList<Payment> Payments { get; set; }
    }
}
