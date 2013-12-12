using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafala.Entities
{
    public class PaymentStatus
    {
        public virtual Guid Id { get; set; }

        public virtual Commitment Commitment { get; set; }

        public virtual DonationCase DonationCase { get; set; }

        public virtual PaymentPeriod PaymentPeriod { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual decimal CommittedAmount { get; set; }

        public virtual decimal PaidAmount { get; set; }

        public virtual bool Paid { get; set; }

        public virtual Guid DonationCaseId { get; set; }
    }
}
