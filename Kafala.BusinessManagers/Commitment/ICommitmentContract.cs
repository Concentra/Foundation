using System;
using Kafala.Entities;

namespace Kafala.BusinessManagers.Commitment
{
    public interface ICommitmentContract
    {
        Guid DonationCaseId { get; set; }
        Guid DonorId { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        decimal Amount { get; set; }
    }
}