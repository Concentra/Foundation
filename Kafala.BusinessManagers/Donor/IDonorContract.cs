using System;
using Kafala.Entities.Enums;

namespace Kafala.BusinessManagers.Donor
{
    public interface IDonorContract
    {
        string Name { get; set; }

        string Email { get; set; }

        string Telephone { get; set; }

        DonorStatus DonorStatus { get; set; }

        Guid ReferralId { get; set; }

        string Mobile { get; set; }

        DateTime? JoinDate { get; set; }
    }
}