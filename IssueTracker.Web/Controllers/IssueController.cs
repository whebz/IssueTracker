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
        private Service.Interface.IIssue issueService;

        private readonly string _cnString;
        #region ctor

        public IssueController()
        {
            _cnString = cf.AppSettings["active-cn"];
            accountService = new Service.Account(_cnString);
            issueService = new Service.Issue(_cnString);
        }

        #endregion

        // GET: Issue
        public ActionResult Index()
        {
            return View("List");
        }
        // GET: Issue/List
        public ActionResult List()
        {
            return View();
        }

        #region datagrid
        public ActionResult Issues_Read([DataSourceRequest] DataSourceRequest request)
        {
            var usr = (User as CustomPrincipal).AccountId;
            return Json(issueService.GetList(usr, null).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Issue_Destroy(int id)
        {
            return Json(issueService.Delete(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Form

        private Models.IssueFormVM GetFormData(int? id)
        {
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
            var role = (User as CustomPrincipal).AccountTypeId;

            string resp = null;
            if (state == "I")
            {
                if (role == "C" && data.IssueStatus == null)
                    data.IssueStatus = "N";
                resp = issueService.Add(data);
            }
            else
                resp = issueService.Update(data);
            if (string.IsNullOrEmpty(resp))
                return RedirectToAction("List", "Issue");
            ViewBag.ErrMsg = System.Text.RegularExpressions.Regex.Replace(resp, @"\r\n?|\n|\t|'", " ");
            int? id = null;
            if (data.Id > 0)
                id = data.Id;
            return View(GetFormData(id));
        }
        #endregion

        // GET: Issue
        public ActionResult Detail(int id)
        {
            var m = issueService.GetById(id);
            return View(m);
        }

        [HttpPost]
        public ActionResult Detail(Model.ViewModel.IssueViewModel data)
        {
            var err = issueService.AssignTo(data.Id, data.AssignedTo, data.Description);
            if (string.IsNullOrEmpty(err))
                return RedirectToAction("List", "Issue");
            ViewBag.ErrMsg = System.Text.RegularExpressions.Regex.Replace(err, @"\r\n?|\n|\t|'", " ");
            return View(issueService.GetById(data.Id));
        }
    }
}