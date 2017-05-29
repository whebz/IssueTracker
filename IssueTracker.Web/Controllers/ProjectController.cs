using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cf = System.Configuration.ConfigurationManager;
namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private Service.Interface.IProject projectService;

        #region ctor

        public ProjectController()
        {
            var cn = cf.AppSettings["active-cn"];
            projectService = new Service.Project(cn);
        }

        #endregion


        // GET: Project
        public ActionResult GetProjectListNoNA()
        {
            var data = projectService.GetList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Project
        public ActionResult GetProjectList()
        {
            var data = projectService.GetList();
            var list = new List<Model.Project> { new Model.Project { Id = 0, Name = "N/A" } };
            list.AddRange(data);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}