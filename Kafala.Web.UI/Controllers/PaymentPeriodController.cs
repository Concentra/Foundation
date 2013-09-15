using System;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Kafala.BusinessManagers.PaymentPeriod;
using Kafala.Query.PaymentPeriod;
using Kafala.Web.ViewModels.PaymentPeriod;

namespace Kafala.Web.UI.Controllers
{
    public class PaymentPeriodController : BaseController
    {
        private readonly IQueryContainer queryContainer;

        private readonly PaymentPeriodBusinessManager businessManager;

        public PaymentPeriodController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManager = businessManagerContainer.Get<PaymentPeriodBusinessManager>();
            this.queryContainer = queryContainer;
        }

        public ActionResult Index()
        {
            var container = this.queryContainer.Get<ListPaymentPeriodViewModelPopulator>();
            var model = container.Execute();
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var container = this.queryContainer.Get<PaymentPeriodCreateModelPopulator>();
            var model = container.Execute(null);
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(CreatePaymentPeriodViewModel model)
        {
            var id = businessManager.Add(model.Year, model.Month, model.Name);
            return RedirectToAction("Details", new {id });
        }


        public ActionResult Details(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<PaymentPeriodViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("View", model);
        }

        public ActionResult Edit(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<UpdatePaymentPeriodViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditPaymentPeriodViewModel model)
        {
            var id = businessManager.Update(model.Id, model.Year, model.Month, model.Name);
            return RedirectToAction("Details", new {id });
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var result = businessManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
