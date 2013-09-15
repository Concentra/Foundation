using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kafala.Entities.Enums;

namespace Kafala.Entities
{
    public class DonationCase
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual DonationCaseStatus DonationCaseStatus { get; set; }
    }
}
