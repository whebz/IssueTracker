using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using IssueTracker.Web.Security;
using cf = System.Configuration.ConfigurationManager;

namespace IssueTracker.Web.Controllers
{
    public class LoginController : Controller
    {
        private Service.Interface.IAccount _AccountContext;

        public LoginController ()
        {
            _AccountContext = new Service.Account(cf.AppSettings["active-cn"]);
        }
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Web.Models.Login model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var uProfile = _AccountContext.Authentication(model.UserId, model.Password);
                if (uProfile != null)
                {
                    var serializeModel = new CustomPrincipalSerializeModel()
                    {
                        AccountId = uProfile.AccountId,
                        Name = uProfile.Name,
                        Email = uProfile.Email,
                        AccountType = uProfile.AccountType,
                        AccountTypeId = uProfile.AccountTypeId
                    };
                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, uProfile.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    if (uProfile.AccountTypeId == "0")
                        return RedirectToAction("Index", "Dashboard");
                    else
                        return RedirectToAction("Issues", "Issue");
                }
                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(model);
        }


    }
}