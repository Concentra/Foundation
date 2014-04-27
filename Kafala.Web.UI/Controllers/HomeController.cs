using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Foundation.Web.Security;
using Kafala.BusinessManagers;
using Kafala.BusinessManagers.User;
using Kafala.Query.Security;
using Kafala.Web.ViewModels.Home;
using StructureMap;

namespace Kafala.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBusinessManagerContainer businessManagerContainer;

        private readonly IQueryContainer queryContainer;

        private readonly IAuthenticationService authenticationService;
        
        public HomeController(IBusinessManagerContainer businessManagerContainer,
            IQueryContainer queryContainer)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
            this.authenticationService = ObjectFactory.GetInstance<AuthenticationService>();
        }

        public ActionResult LogOn()
        {
            return View("LogOn");
        }
        
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model)
        {
            var signInResult = this.authenticationService.SignIn(model.UserName, model.Password);

            if(signInResult == SignInResult.Success )
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
              
                if(!string.IsNullOrEmpty(model.ReturnURL))
                {
                    return Redirect(model.ReturnURL);
                }
                else
                {
                   return RedirectToAction("Index", "Donor");
                }
            }
            else
            {
                return View("LogOn", model);
            }
        }

        public ActionResult ForgotPassword(string email)
        {
            var user = businessManagerContainer.Get<UserManager>();
            user.SendPasswordReminderEmail(email);
            return RedirectToAction("LogOn");
        }

        public ActionResult LogOff()
        {
            this.authenticationService.SignOut();
            return RedirectToAction("LogOn");
        }
    }
}
