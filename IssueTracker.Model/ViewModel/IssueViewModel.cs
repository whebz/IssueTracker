using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model.ViewModel
{
    public class IssueViewModel : Issue
    {
        public string StatusDescription { get; set; }
        public string Priority { get; set; }
        public string AssignedToName { get; set; }
        public string CreateByName { get; set; }
        public string ProjectName { get; set; }
    }
}
