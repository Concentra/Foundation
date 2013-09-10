using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;

namespace Kafala.Web.UI.Controllers
{
    public class CommitmentController : BaseController
    {
        private readonly IBusinessManagerContainer businessManagerContainer;

        private readonly IQueryContainer queryContainer;

        public CommitmentController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
        }

        

        public ActionResult Index()
        {
            return View();
        }

    }
}
