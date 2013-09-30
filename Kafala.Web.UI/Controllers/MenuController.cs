
using System;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Foundation.Web.Navigation;
using Kafala.BusinessManagers.DonationCase;
using Kafala.Query.Shared;
using Kafala.Web.ViewModels.DonationCase;

namespace Kafala.Web.UI.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IQueryContainer queryContainer;

        public MenuController(IBusinessManagerContainer businessManagerContainer, IQueryContainer queryContainer)
        {
            this.queryContainer = queryContainer;
        }

 
        public ActionResult Index()
        {
            var container = this.queryContainer.Get<MenuPopulator>();
            var model = container.Execute(null);
            return Content((new MenuBuilder()).Build(model).ToString());
        }

        public ActionResult InPage()
        {

            return View();
        }
    }
}
