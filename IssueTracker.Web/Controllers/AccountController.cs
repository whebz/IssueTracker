﻿using System;
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
            return View();
        }

        // GET: Account/Edit
        public ActionResult Edit(string k)
        {
            return View("Editor/AccountEditor", accountService.GetById(k));
        }

        public ActionResult Accounts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(accountService.GetList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Picture(string id)
        {
            if (id != null)
            {
                return base.File("../../Content/contacts/" + id + ".jpg", "image/jpeg");
            }
            else
            {
                return base.File("../../Content/contacts/" + "default-contact.png", "image/jpeg");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Account_Create([DataSourceRequest] DataSourceRequest request, Model.Account account)
        {
            var results = new List<Model.Account>();

            if (account != null && ModelState.IsValid)
            {
                accountService.Add(account);

                return Json(new[] { account }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                return Json(ModelState.ToDataSourceResult());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Account_Update([DataSourceRequest] DataSourceRequest request, Model.Account account)
        {
            if (account != null && ModelState.IsValid)
            {
                accountService.Update(account);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Account_Destroy([DataSourceRequest] DataSourceRequest request, Model.Account contact)
        {
            if (contact != null)
            {
                accountService.Delete(contact.AccountId);
            }

            return Json(ModelState.ToDataSourceResult());
        }
        [HttpPost]
        public ActionResult UploadPhoto(IEnumerable<HttpPostedFileBase> files, string msgid)
        {
            // The Name of the Upload component is "files"

                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/files/profile"), fileName);

                    // The files are not actually saved in this demo
                     file.SaveAs(physicalPath);
                }
     


            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemovePhoto(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/files/profile"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult GetClient()
        {
            var data = clientService.GetList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountType()
        {
            var data = accountService.GetAccountTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
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
    }
}