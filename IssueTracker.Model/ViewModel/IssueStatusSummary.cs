using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model.ViewModel
{
    public class IssueStatusSummary
    {
        public int Closed { get; set; }
        public int Assigned { get; set; }
        public int NotAssigned { get; set; }
        public int Suspended { get; set; }
        public int OnProgress { get; set; }
    }
}
