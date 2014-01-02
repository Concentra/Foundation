using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Donor;
using Kafala.Entities.Enums;

namespace Kafala.Web.ViewModels.Donor
{
    public class DonorUpdateViewModel : IDonorContract
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public Guid Id { get; set; }

        [EditControl( ElementType = ElementType.Text)]
        [Display(Order = 1, Name = "Donor Name", Prompt = "The full donor name")]
        [Required]
        public string Name { get; set; }

        [Display(GroupName = "Contact Info", Order = 2, Name = "Email", Prompt = "Enter email address")]
        [EditControl(ElementType = ElementType.Text)]
        [Required]
        public string Email { get; set; }

        [Display(GroupName = "Contact Info", Order = 3, Name = "Telephone", Prompt = "Enter Telephone Number")]
        [EditControl(ElementType = ElementType.Text)]
        public string Telephone { get; set; }

        [EditControl(ElementType = ElementType.Enum)]
        [Display(Prompt = "Select Status")]
        public DonorStatus DonorStatus { get; set; }

        [Display(GroupName = "Contact Info")]
        [EditControl(ElementType = ElementType.Text)]
        public string Mobile { get; set; }

        [Display(Name = "Date Joined", Order = 1)]
        [EditControl(ElementType = ElementType.DateTime)]
        public DateTime? JoinDate { get; set; }

        public Guid ReferralId { get; set; }
    }
}
