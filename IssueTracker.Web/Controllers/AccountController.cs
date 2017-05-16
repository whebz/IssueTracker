using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using cf = System.Configuration.ConfigurationManager;

namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private Service.Interface.IAccount accountService;
        private Service.Interface.IClient clientService;

        #region ctor

        public AccountController()
        {
            var cn = cf.AppSettings["active-cn"];
            accountService = new Service.Account(cn);
            clientService = new Service.Client(cn);
        }

        #endregion
        private void PopulateClient()
        {
            var cli = clientService.GetList();
            ViewData["Clients"] = cli;
            ViewData["defaultClient"] = cli.First();
        }
        // GET: Account/List
        public ActionResult List()
        {
            PopulateClient();
            var data = accountService.GetAccountSummary();
            return View(data);
        }

        // GET: Account/Edit
        public ActionResult Edit(string k)
        {
            Models.AccountEditorVM data;
            if (string.IsNullOrEmpty(k))
                data = new Models.AccountEditorVM { ActionState = "I", Account = new Model.Account() };
            else
                data = new Models.AccountEditorVM { ActionState = "U", Account = accountService.GetById(k) };
            ViewBag.AccountSummary = accountService.GetAccountSummary();
            return View("Editor/AccountEditor", data);
        }

        [HttpPost]
        public ActionResult Edit(Model.Account data, string state)
        {
            string resp = null;
            if (state == "I")
                resp = accountService.Add(data);
            else
                resp = accountService.Update(data);
            if (string.IsNullOrEmpty(resp))
                return RedirectToAction("List", "Account");
            ViewBag.ErrMsg = resp;
            ViewBag.AccountSummary = accountService.GetAccountSummary();
            return View("Editor/AccountEditor", data);
        }


        public ActionResult Accounts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(accountService.GetList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Account_Destroy(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return Json(accountService.Delete(id), JsonRequestBehavior.AllowGet);
            }

            return Json("ID NULL", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClient()
        {
            var data = clientService.GetList();
            var list = new List<Model.Client> { new Model.Client { Id = -1, Name = "N/A" } };
            list.AddRange(data);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountType()
        {
            var data = accountService.GetAccountTypeList();
            var list = new List<Model.AccountType> { new Model.AccountType { Id = null, Description = "N/A" } };
            list.AddRange(data);            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountSummary()
        {
            var data = accountService.GetAccountSummary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        #region Partial Views
        public ActionResult _ListSummary()
        {
            return PartialView();
        }
        #endregion

    }
}