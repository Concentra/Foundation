using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foundation.Infrastructure.Query;
using Foundation.Web.Paging;
using Kafala.Web.ViewModels.Reports.Partials;

namespace Kafala.Web.UI.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IQueryContainer queryContainer;

        public ReportsController(IQueryContainer queryContainer)
        {
            this.queryContainer = queryContainer;
        }

        [RenderPagedView]
        public ActionResult OverDue(FilterPaymentStatus filter)
        {
            var model = this.queryContainer.Get<Query.Reports.PaymentStatusReportViewModelPopulator>().Execute(filter);
            
            return View("OverDure", model);
        }

    }
}
