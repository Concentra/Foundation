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

        [EditControl(Order = 1, ElementType = ElementType.Text, Label = "Donor Name", PromptText = "The full donor name")]
        [Required]
        public string Name { get; set; }

        [EditControl(GroupName = "Contact Info", Order = 2, ElementType = ElementType.Text, Label = "Email", PromptText = "Enter email address")]
        [Required]
        public string Email { get; set; }

        [EditControl(GroupName = "Contact Info", Order = 3, ElementType = ElementType.Text, Label = "Telephone", PromptText = "Enter Telephone Number")]
        public string Telephone { get; set; }

        [EditControl(ElementType = ElementType.Enum, PromptText = "Select Status")]
        public DonorStatus DonorStatus { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.Text)]
        public string Mobile { get; set; }

        [EditControl(Label = "Date Joined", ElementType = ElementType.DateTime, Order = 1)]
        public DateTime? JoinDate { get; set; }

        public Guid ReferralId { get; set; }
    }
}
