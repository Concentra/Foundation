using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [EditControl(GroupName = "Personal Info", Order = 10, ElementType = ElementType.StaticText, Label = "Donor Name", PromptText = "The full donor name")]
        [Required]
        public string Name { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.Text, Label = "Email", PromptText = "Enter email address")]
        [Required]
        public string Email { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.Text, Label = "Telephone", PromptText = "Enter Telephone Number")]
        public string Telephone { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.Enum, PromptText = "Enter Telephone Number")]
        public DonorStatus DonorStatus { get; set; }

        public Guid ReferralId { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.List, PromptText = "Enter Telephone Number")]
        [CollectionInfo(ListSourceMember = "ListProperty")]
        public DonorStatus SelectedItem { get; set; }

        public IEnumerable<SelectListItem> ListProperty { get; set; }

        [EditControl(GroupName = "Contact Info", ElementType = ElementType.Text)]
        public string Mobile { get; set; }

        [EditControl(Label = "Date Joined", ElementType = ElementType.DateTime, Order = 1)]
        public DateTime? JoinDate { get; set; }
    }
}
