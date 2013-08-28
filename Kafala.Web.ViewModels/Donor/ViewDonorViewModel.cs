using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Kafala.Web.ViewModels.Donor
{
    public class ViewDonorViewModel
    {
        [Key]
        public Guid? Id { get; set; }

        [Display(Name = "Donor Name")]
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Email Address..")]
        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [DataType(DataType.Text)]
        public string Telephone { get; set; }

        [Display(Name = "Date Joined")]
        public DateTime JoinDate { get; set; }

        public Guid ReferralId { get; set; }

        public string Mobile { get; set; }
    }
}
