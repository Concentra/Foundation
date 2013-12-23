using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Kafala.Entities;

namespace Kafala.BusinessManagers.Commitment
{
    public interface ICommitmentContract
    {
        [Required(ErrorMessage = "Please select a donation case.")]
        Guid DonationCaseId { get; set; }
        
        [Required(ErrorMessage = "Please select a donor.")]
        Guid DonorId { get; set; }
        
        [Required(ErrorMessage = "Please select a start date.")]
        [DataType(DataType.Date, ErrorMessage = "Please select a valid date")]
        DateTime? StartDate { get; set; }
        
        DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Please select a commitment amount.")]
        [Numeric(ErrorMessage = "Please select a valid Amount")]
        [Min(1)]
        decimal Amount { get; set; }
    }
}