using System;
using Kafala.Entities.Enums;

namespace Kafala.BusinessManagers.DonationCase
{
    public interface IDonationCaseContract
    {
        string Name { get; set; }

        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

        DonationCaseStatus DonationCaseStatus { get; set; }
    }
}