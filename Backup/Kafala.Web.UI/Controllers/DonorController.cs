using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Kafala.Query.Donor;
using Kafala.Web.ViewModels.Donor;

namespace Kafala.Web.UI.Controllers
{
    public class DonorController : BaseController
    {
        protected readonly IBusinessManagerContainer businessManagerContainer;

        protected readonly IQueryContainer queryContainer;

        protected readonly IFlashMessenger flashMessenger;
        
        //
        // GET: /Donor/
        public DonorController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer, IFlashMessenger flashMessenger)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
            this.flashMessenger = flashMessenger;
        }


        public ActionResult Index()
        {
            var container = this.queryContainer.Get<DonorListModelPopulator>();
            var model = container.Execute(null);
            return View(model);
        }

    }
}
