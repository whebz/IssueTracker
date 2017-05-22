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
    public class IssueController : Controller
    {
        private Service.Interface.IAccount accountService;
        private Service.Interface.IClient clientService;
        private Service.Interface.IIssue issueService;

        #region ctor

        public IssueController()
        {
            var cn = cf.AppSettings["active-cn"];
            accountService = new Service.Account(cn);
            clientService = new Service.Client(cn);
            issueService = new Service.Issue(cn);
        }

        #endregion

        // GET: Issue
        public ActionResult Index()
        {
            return View("List");
        }

        #region datagrid
        public ActionResult Issues_Read([DataSourceRequest] DataSourceRequest request, Models.IssueListFilterVM filter)
        {
            
            return Json(issueService.GetList(User.Identity.Name,null).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetStatusStats(Models.IssueListFilterVM filter)
        {
            var data = issueService.GetStatusStats(null, null);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}