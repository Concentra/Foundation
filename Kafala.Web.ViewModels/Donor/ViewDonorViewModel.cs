using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.Entities.Enums;
using Kafala.Web.ViewModels.Commitment;

namespace Kafala.Web.ViewModels.Donor
{
    public class ViewDonorViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public Guid Id { get; set; }

        [EditControl( ElementType = ElementType.StaticText)]
        [Display(GroupName = "Personal Info", Order = 10, Name = "Donor Name", Prompt = "The full donor name")]
        [Required]
        public string Name { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        [Display(GroupName = "Contact Info", Name = "Email", Prompt = "Enter email address")]
        [Required]
        public string Email { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        [Display(GroupName = "Contact Info", Name = "Telephone", Prompt = "Enter Telephone Number")]
        public string Telephone { get; set; }

        [Display(GroupName = "Contact Info", Prompt = "Enter Telephone Number")]
        [EditControl(ElementType = ElementType.Enum)]
        public DonorStatus DonorStatus { get; set; }

        public Guid ReferralId { get; set; }

        public IEnumerable<SelectListItem> ListProperty { get; set; }

        [Display(GroupName = "Contact Info")]
        [EditControl(ElementType = ElementType.Text)]
        public string Mobile { get; set; }

        [Display(Name = "Date Joined", Order = 1)]
        [EditControl( ElementType = ElementType.DateTime)]
        public DateTime? JoinDate { get; set; }

        public DonorDashBoard DonorDashBoard  { get; set; }
    }
}
