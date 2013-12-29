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

        private readonly IBusinessManagerContainer businessManagerContainer;

        public PaymentPeriodController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
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
            var id = businessManagerContainer.Get<PaymentPeriodBusinessManager>().Add(model.Year, model.Month, model.Name);
            return RedirectToAction("Details", new {id });
        }


        public ActionResult Details(Guid id)
        {
            System.Threading.Thread.Sleep(3000);
         
            var modelPopulator = this.queryContainer.Get<PaymentPeriodViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("View", model);
            }
            else
            {
                return View("View", model);    
            }
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
            var id = businessManagerContainer.Get<PaymentPeriodBusinessManager>().Update(model.Id, model.Year, model.Month, model.Name);
            return RedirectToAction("Details", new {id });
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            System.Threading.Thread.Sleep(3000);
            var result = businessManagerContainer.Get<PaymentPeriodBusinessManager>().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
