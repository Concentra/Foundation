using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.Donor;
using Kafala.Entities.Enums;

namespace Kafala.Web.ViewModels.Donor
{
    
    public class CreateDonorViewModel : IDonorContract
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public Guid? Id { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        [Display(Prompt  = "The full donor name" , Name  = "Donor Name" , Order = 1)]
        [Required(ErrorMessage = "Address is required")]
        public string Name { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        [Display(GroupName = "Contact Info", Order = 2, Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Please enter a valid email")]
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
