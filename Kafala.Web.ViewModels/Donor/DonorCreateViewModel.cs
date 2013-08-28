using System;
using System.ComponentModel.DataAnnotations;


namespace Kafala.Web.ViewModels.Donor
{
    public class DonorCreateViewModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Donor Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Email Address..")]
        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Date Joined")]
        public DateTime JoinDate { get; set; }

        public Guid ReferralId { get; set; }

        public string Mobile { get; set; }
    }
}
