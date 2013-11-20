using System;
using System.Collections.Generic;
using System.Text;

namespace Kafala.Entities
{
    public class PaymentPeriod
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Year { get; set; }

        public virtual int Month { get; set; }
    }
}
