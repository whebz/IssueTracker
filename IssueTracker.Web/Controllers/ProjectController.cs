using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cf = System.Configuration.ConfigurationManager;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace IssueTracker.Web.Controllers
{
    using Security;

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

        #region datagrid
        public ActionResult Project_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = projectService.GetList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
            var role = (User as CustomPrincipal).AccountTypeId;
            IEnumerable<Model.Project> data = null;
            data = projectService.GetList();
            var list = new List<Model.Project> { new Model.Project { Id = 0, Name = "N/A" } };
            list.AddRange(data);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}