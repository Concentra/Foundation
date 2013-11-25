using System;

namespace Kafala.BusinessManagers.Payment
{
    public interface IPaymentContract
    {
        string Comments { get; set; }

        DateTime? PaymentDate { get; set; }

        Guid PaymentPeriodId { get; set; }

        Guid CommitmentId { get; set; }

        decimal Amount { get; set; }
    }
}