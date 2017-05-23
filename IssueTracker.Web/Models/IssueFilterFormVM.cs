using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class IssueFilterFormVM
    {
        public IEnumerable<Model.Priority> Priority { get; set; }
    }
}