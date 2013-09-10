using System;
using System.Web.Mvc;
using System.Web.Routing;
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

        //
        // GET: /Donor/
        public DonorController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
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
        public ActionResult Create(CreateDonorViewModel model)
        {
            var manager = businessManagerContainer.Get<DonorBusinessManager>();
            var donorId = manager.Add(model);
            return RedirectToAction("Details", new { id = donorId });
        }

      
        public ActionResult Details(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<DonorViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("View", model);
        }

        public ActionResult Edit(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<UpdateDonorViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(DonorUpdateViewModel model)
        {
            var manager = businessManagerContainer.Get<DonorBusinessManager>();
            var donorId = manager.Update(model.Id, model);
            return RedirectToAction("Details", new { id = donorId });
        }

        [HttpPost]
        public ActionResult Delete(Guid donorId)
        {
            var manager = businessManagerContainer.Get<DonorBusinessManager>();
            var result = manager.Delete(donorId);
            return RedirectToAction("Index");
        }
    }
}
