using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model
{
    public class ClientProject
    {
        /// <summary>
        /// FK (Client)
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// FK (Project)
        /// </summary>
        public int ProjectId { get; set; }
    }
}
