﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Web;
using Foundation.Web.Security;
using Kafala.BusinessManagers;
using Kafala.Web.ViewModels.Home;

namespace Kafala.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBusinessManagerContainer businessManagerContainer;

        private readonly IQueryContainer queryContainer;

        private readonly IFormAuthenticationService formAuthenticationService;

        public HomeController(IBusinessManagerContainer businessManagerContainer,
            IQueryContainer queryContainer, 
            IFormAuthenticationService formAuthenticationService)
        {
            this.businessManagerContainer = businessManagerContainer;
            this.queryContainer = queryContainer;
            this.formAuthenticationService = formAuthenticationService;
        }

        public ActionResult LogOn()
        {
            return View("LogOn");
        }
        
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model)
        {
            var signInResult = this.formAuthenticationService.SignIn(model.UserName, model.Password);

            if(signInResult == SignInResult.Success )
            {
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
                this.FlashMessenger.AddMessage("Wrong Login", FlashMessageType.Failure);
                return View("LogOn", model);
            }
        }

        public ActionResult ForgotPassword()
        {
            var notManager = new AccountNotificationManager();
            notManager.SendPasswordReminderEmail("timur.ozuns@concentra.co.uk");
            return RedirectToAction("LogOn");
        }
    }
}
