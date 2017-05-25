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
using IssueTracker.Web.Security;

namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private Service.Interface.IAccount accountService;
        private Service.Interface.IClient clientService;
        private Service.Interface.IIssue issueService;
        private readonly string _cnString;
        #region ctor

        public IssueController()
        {
            _cnString = cf.AppSettings["active-cn"];
            accountService = new Service.Account(_cnString);
            clientService = new Service.Client(_cnString);
            issueService = new Service.Issue(_cnString);
        }

        #endregion

        // GET: Issue
        public ActionResult Index()
        {
            var filter = GetListVM();
            return View("List", filter);
        }
        // GET: Issue/List
        public ActionResult List()
        {
            var filter = GetListVM();
            return View(filter);
        }

        private Models.IssueFilterFormVM GetListVM()
        {
            return new Models.IssueFilterFormVM(_cnString, (User as CustomPrincipal).ClientId);
        }
        #region datagrid
        public ActionResult Issues_Read([DataSourceRequest] DataSourceRequest request, Models.IssueListFilterVM filter)
        {
            var usr = (User as CustomPrincipal).AccountId;
            return Json(issueService.GetList(usr, null).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetStatusStats(Models.IssueListFilterVM filter)
        {
            var data = issueService.GetStatusStats(null, null);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region Form

        public ActionResult IssueForm(int? id)
        {
            var model = new Models.IssueFormVM();
            model.Issue = new Model.Issue();

            if (id.HasValue)
                ;
            return View(model);
        }

        #endregion
    }
}