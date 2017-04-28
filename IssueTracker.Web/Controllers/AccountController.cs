using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using cf = System.Configuration.ConfigurationManager;

namespace IssueTracker.Web.Controllers
{
    public class AccountController : Controller
    {
        private Service.Interface.IAccount accountService;

        #region ctor

        public AccountController()
        {
            accountService = new Service.Account(cf.AppSettings["active-cn"]);
        }

        #endregion

        // GET: Account/List
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Accounts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(accountService.GetList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}