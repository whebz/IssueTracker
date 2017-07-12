using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using cf = System.Configuration.ConfigurationManager;

namespace IssueTracker.Web.Controllers
{
    public class CommonController : Controller
    {

        public CommonController ()
        {
            _cnString = cf.AppSettings["active-cn"];
            IssueStatusService = new Service.IssueStatus(_cnString);
            priorityService = new Service.Priority(_cnString);
            projectService = new Service.Project(_cnString);
        }

        private readonly string _cnString;

        private readonly Service.Interface.IIssueStatus IssueStatusService;
        private readonly Service.Interface.IPriority priorityService;
        private readonly Service.Interface.IProject projectService;

        public ActionResult GetIssueStatusList()
        {
            var data = IssueStatusService.GetList();
            var list = new List<Model.IssueStatus> { new Model.IssueStatus { Id = null, Description = "N/A" } };
            list.AddRange(data);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriorityList()
        {
            var data = priorityService.GetList();
            var list = new List<Model.Priority> { new Model.Priority { Id = 0, Name = "N/A" } };
            list.AddRange(data);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List(int? idx)
        {
            ViewBag.Index = idx ?? 0;
            return View();
        }

        public ActionResult CreateOrEdit(int idx, int? id)
        {
            Models.CommonFormVM data;
            if (id.HasValue)
            {
                Model.Base.INSModel m;
                m = projectService.GetList().Where(c => c.Id == id.Value).FirstOrDefault();
                data = new Models.CommonFormVM(idx, m);
            }
            else
            {
                data = new Models.CommonFormVM(idx);
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult CreateOrEdit(int idx, Model.Base.INSModel m, string state)
        {
            var err = "";

            if (state == "I")
                err = projectService.Add(new Model.Project { Name = m.Name, Suspended = m.Suspended });
            else
                err = projectService.Update(new Model.Project { Name = m.Name, Suspended = m.Suspended, Id = m.Id });

            if (string.IsNullOrEmpty(err))
                return RedirectToAction("List", "Common", idx);
            return View(m);
        }
    }
}