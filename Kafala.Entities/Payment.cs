using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kafala.Entities
{
    public class Payment
    {
        public virtual Guid Id { get; set; }

        public virtual string Comments { get; set; }

        public virtual DateTime? PaymentDate { get; set; }

        public virtual PaymentPeriod PaymentPeriod { get; set; }

        public virtual Commitment Commitment { get; set; }

        public virtual Decimal Amount { get; set; }
    }
}
