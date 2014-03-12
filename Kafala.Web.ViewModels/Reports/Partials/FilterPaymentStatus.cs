using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.Web.CustomAttribute;
using Foundation.Web.Paging;
using NHibernate.Mapping;

namespace Kafala.Web.ViewModels.Reports.Partials
{
    public class FilterPaymentStatus :  PagingInfoViewModel
    {
        [FilterControl(DataElement = "Commitment.Donor.Id")]
        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "DonorList", SelectPromptValue = null, SelectPromptLabel = "All")]
        [Display(Name = "Donor")]
        public Guid DonorId { get; set; }

        public IEnumerable<SelectListItem> DonorList { get; set; }

        [EditControl(ElementType = ElementType.List)]
        [CollectionInfo(ListSourceMember = "PeriodList", SelectPromptValue = null, SelectPromptLabel = "All")]
        [FilterControl(DataElement = "PaymentPeriod.Id")]
        [Display(Name = "Period")]
        public Guid PaymentPeriodId { get; set; }

        public IEnumerable<SelectListItem> PeriodList { get; set; }
        
        
        [CollectionInfo(ListSourceMember = "YearList")]
        [FilterControl(DataElement = "PaymentPeriod.Year")]
        public int PaymentPeriodYear { get; set; }

        public IEnumerable<SelectListItem> YearList { get; set; }

        [EditControl(ElementType = ElementType.DateTime)]
        public DateTime? PointInTime { get; set; }
    }
}
