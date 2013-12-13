using System;
using System.Collections.Generic;
using Kafala.Web.ViewModels.Commitment;
using Kafala.Web.ViewModels.Payment;
using Kafala.Web.ViewModels.Reports;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorDashBoard
    {
        public Guid DonorId { get; set; }

        public IEnumerable<ViewCommitmentViewModel> Commitments { get; set; }

        public IEnumerable<ViewPaymentViewModel> Payments { get; set; }

        public IEnumerable<OverDuePaymentViewModel> OutStanding { get; set; }
    }
}