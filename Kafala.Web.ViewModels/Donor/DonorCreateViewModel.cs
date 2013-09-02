using System;
using System.ComponentModel.DataAnnotations;
using Kafala.Entities.Enums;
using VortexSoft.Bootstrap.CustomAttribute;


namespace Kafala.Web.ViewModels.Donor
{
    
    public class DonorCreateViewModel
    {
        [DynamicControl(Control = ControlType.Hidden)]
        public Guid? Id { get; set; }

        [DynamicControl(GroupName = "Personal Info", Order = 10,Control = ControlType.StaticText, Label = "Donor Name", PromptText = "The full donor name")]
        [Required]
        public string Name { get; set; }

        [DynamicControl(GroupName = "Contact Info", Control = ControlType.TextBox, Label = "Email", PromptText = "Enter email address")]
        [Required]
        public string Email { get; set; }

        [DynamicControl(GroupName = "Contact Info", Control = ControlType.TextBox, Label = "Telephone", PromptText = "Enter Telephone Number")]
        public string Telephone { get; set; }

        [DynamicControl(GroupName = "Contact Info", Control = ControlType.Enum, PromptText = "Enter Telephone Number")]
        public DonorStatus DonorStatus { get; set; }

        public Guid ReferralId { get; set; }

        [DynamicControl(GroupName = "Contact Info", Control = ControlType.TextBox)]
        public string Mobile { get; set; }

        [DynamicControl(Label = "Date Joined", Control = ControlType.DateTime, Order = 1)]
        public DateTime? JoinDate { get; set; }
    }
}
