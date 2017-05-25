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
            ClientService = new Service.Client(_cnString);
            IssueStatusService = new Service.IssueStatus(_cnString);
        }

        private readonly string _cnString;

        private readonly Service.Interface.IClient ClientService;

        private readonly Service.Interface.IIssueStatus IssueStatusService;

        public ActionResult GetClients()
        {
            var data = ClientService.GetList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIssueStatus()
        {
            var data = IssueStatusService.GetList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}