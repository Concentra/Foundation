using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kafala.Entities.Enums;

namespace Kafala.Entities
{
    public class Donor
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Telephone { get; set; }

        public virtual DateTime? JoinDate { get; set; }

        public virtual Donor Referral { get; set; }

        public virtual DonorStatus DonorStatus { get; set; }

        public virtual IList<Commitment> Commitments { get; set; }
    }
}
