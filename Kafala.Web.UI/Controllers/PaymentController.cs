
using System;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Foundation.Web.Paging;
using Kafala.BusinessManagers.Payment;
using Kafala.Query.Payment;
using Kafala.Web.ViewModels.Payment;
using Kafala.Web.ViewModels.Payment.Partial;

namespace Kafala.Web.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IQueryContainer queryContainer;

        private readonly IBusinessManagerContainer businessManagerContainer;

        public PaymentController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
        }

        [RenderPagedView]
        public ActionResult Index(PaymentFilterViewModel parameters)
        {
            var container = this.queryContainer.Get<PaymentListModelPopulator>();
            var model = container.Execute(parameters);
            
            return View("Index", model);
        }

        public ActionResult Create(Guid? id)
        {
            var container = this.queryContainer.Get<PaymentCreateModelPopulator>();
            var model = container.Execute(id);
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(CreatePaymentViewModel model)
        {
            var businessManager = businessManagerContainer.Get<PaymentBusinessManager>();
            var id = businessManager.Register(model);
            return RedirectToAction("Details", new {id });
        }


        public ActionResult Details(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<PaymentViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return AdaptiveView("View", model);
        }

        public ActionResult Edit(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<UpdatePaymentViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditPaymentViewModel model)
        {
            var businessManager = businessManagerContainer.Get<PaymentBusinessManager>();
            var id = businessManager.Update(model.Id, model);
            return RedirectToAction("Details", new {id });
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var businessManager = businessManagerContainer.Get<PaymentBusinessManager>();
            var result = businessManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
