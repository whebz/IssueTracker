using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
namespace IssueTracker.Web.Models
{
    public class IssueFilterFormVM
    {
        public IEnumerable<SelectListItem> Priority { get; set; }
        public IEnumerable<SelectListItem> Client { get; set; }
        public IEnumerable<SelectListItem> Project { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }

        public IssueFilterFormVM(string cn, int client) 
        {
            var queryService = new Service.SqlQuery(cn);
            Priority = queryService.GetList<SelectListItem>("PrioritySelectList");
            DynamicParameters p = null;
            p = new DynamicParameters();
            p.Add("Client", client);

            if (client == 0)
                Client = queryService.GetList<SelectListItem>("ClientSelectList");

            Project = queryService.GetList<SelectListItem>("ProjectSelectList", p);
            Status = queryService.GetList<SelectListItem>("IssueStatusSelectList");
        }
    }
}