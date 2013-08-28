using System;
using System.ComponentModel.DataAnnotations;


namespace Kafala.Web.ViewModels.Donor
{
    
    public class DonorCreateViewModel
    {
        [KeyAttribute]
        public Guid? Id { get; set; }

        [Display(Name = "Donor Name")]
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Email Address..")]
        [Required]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [DataType(DataType.Text)]
        public string Telephone { get; set; }

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        public Guid ReferralId { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.Text)]
        public string Mobile { get; set; }
    }
}
