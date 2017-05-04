using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model.ViewModel
{
    public class ClientProjectViewModel : ClientProject
    {
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
    }
}
