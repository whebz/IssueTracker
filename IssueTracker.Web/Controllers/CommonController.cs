using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        }

        private readonly string _cnString;

        private readonly Service.Interface.IIssueStatus IssueStatusService;
        private readonly Service.Interface.IPriority priorityService;


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
    }
}