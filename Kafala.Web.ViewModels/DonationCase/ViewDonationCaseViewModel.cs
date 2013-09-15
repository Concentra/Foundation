using System;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.Entities.Enums;

namespace Kafala.Web.ViewModels.DonationCase
{
    public class ViewDonationCaseViewModel
    {
        [EditControl(ElementType = ElementType.Hidden)]
        public virtual Guid Id { get; set; }

        [EditControl(ElementType = ElementType.Text)]
        public virtual string Name { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? StartDate { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public virtual DateTime? EndDate { get; set; }

        [EditControl(ElementType = ElementType.Enum)]
        public virtual DonationCaseStatus DonationCaseStatus { get; set; }

    }
}
