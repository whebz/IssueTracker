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
    public class DashboardController : Controller
    {
        private Service.Interface.IIssue issueService;

        private readonly string _cnString;

        #region ctor

        public DashboardController()
        {
            _cnString = cf.AppSettings["active-cn"];
            issueService = new Service.Issue(_cnString);
        }

        #endregion

        // GET: Dashboard
        public ActionResult Index()
        {
            var stats = issueService.GetStatusStats().ToList();
            var data = new Models.DashboardVM(stats);

            return View(data);
        }

        public ActionResult Latest_Issues_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(issueService.GetLatest().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}