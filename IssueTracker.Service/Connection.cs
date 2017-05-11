using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service
{
    internal class Connection
    {
        public static string Get(string cn)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[cn].ConnectionString;
        }
    }
}
