
using System;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Foundation.Web.Paging;
using Kafala.BusinessManagers.Commitment;
using Kafala.Query.Commitment;
using Kafala.Web.ViewModels.Commitment;

namespace Kafala.Web.UI.Controllers
{
    public class CommitmentController : BaseController
    {
        private readonly IQueryContainer queryContainer;

        
        private readonly IBusinessManagerContainer businessManagerContainer;

        public CommitmentController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
        }

        [RendersPagedView]
        public ActionResult Index(CommitmentsListParameters parameters)
        {
            var container = this.queryContainer.Get<CommitmentListModelPopulator>();
            var model = container.Execute(parameters);
            return View("Index", model);
        }

        public ActionResult Create()
        {
            var container = this.queryContainer.Get<CommitmentCreateModelPopulator>();
            var model = container.Execute(null);
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(CreateCommitmentViewModel model)
        {
            var id = businessManagerContainer.Get<CommitmentBusinessManager>().Add(model);
            return RedirectToAction("Details", new {id });
        }


        public ActionResult Details(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<CommitmentViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("View", model);
        }

        public ActionResult Edit(Guid id)
        {
            var modelPopulator = this.queryContainer.Get<UpdateCommitmentViewModelPopulator>();
            var model = modelPopulator.Execute(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditCommitmentViewModel model)
        {
            var id = businessManagerContainer.Get<CommitmentBusinessManager>().Update(model.Id, model);
            return RedirectToAction("Details", new {id });
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var result = businessManagerContainer.Get<CommitmentBusinessManager>().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
