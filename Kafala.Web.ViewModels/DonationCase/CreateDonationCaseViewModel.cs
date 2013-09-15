using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Kafala.BusinessManagers.DonationCase;
using Kafala.Entities.Enums;

namespace Kafala.Web.ViewModels.DonationCase
{
    public class CreateDonationCaseViewModel : IDonationCaseContract
    {
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
