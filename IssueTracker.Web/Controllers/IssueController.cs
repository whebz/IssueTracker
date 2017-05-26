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

        private Models.IssueFormVM GetFormData(int? id)
        {
            var data = GetListVM();
            var model = new Models.IssueFormVM()
            {
                HeaderText = "New Issues",
                Issue = new Model.Issue()
            };
            
            if (id.HasValue)
            {
                model.HeaderText = "Edit Issue";
                model.Issue = issueService.GetById(id.Value);
            }
            else
                model.Issue.CreateBy = (User as CustomPrincipal).AccountId;
            var account = accountService.GetById(model.Issue.CreateBy);
            model.Author = account?.Name;
            return model;
        }

        public ActionResult IssueForm(int? id)
        {       
            return View(GetFormData(id));
        }

        [HttpPost]
        public ActionResult IssueForm(Model.Issue data, string state)
        {
            string resp = null;
            if (state == "I")
                resp = issueService.Add(data);
            else
                resp = issueService.Update(data);
            if (string.IsNullOrEmpty(resp))
                return RedirectToAction("List", "Issue");
            ViewBag.ErrMsg = resp;
            int? id = null;
            if (data.Id > 0)
                id = data.Id;
            return View(GetFormData(id));
        }
        #endregion
    }
}