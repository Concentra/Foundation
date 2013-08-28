using System;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Kafala.BusinessManagers.Donor;
using Kafala.Query.Donor;
using Kafala.Web.ViewModels.Donor;

namespace Kafala.Web.UI.Controllers
{
    public class DonorController : BaseController
    {
        private readonly IBusinessManagerContainer businessManagerContainer;

        private readonly IQueryContainer queryContainer;

        private readonly IFlashMessenger flashMessenger;
        
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
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var container = this.queryContainer.Get<DonorCreateModelPopulator>();
            var model = container.Execute(null);
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(DonorCreateViewModel model)
        {
            var manager = businessManagerContainer.Get<DonorBusinessManager>();
            var donor = manager.AddDonor(model.Name, model.Mobile, model.JoinDate, model.ReferralId);
            return RedirectToAction("View", donor.Id);
        }

        public ActionResult View(Guid guid)
        {
            var container = this.queryContainer.Get<DonorViewModelPopulator>();
            var model = container.Execute(guid);
            return View("View", model);
        }
    }
}
