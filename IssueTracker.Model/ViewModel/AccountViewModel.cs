using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model.ViewModel
{
    public class AccountViewModel: Account
    {
        public string ClientName { get; set; }
        public string AccountType { get; set; }
    }
}
