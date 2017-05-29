using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class IssueListFilterVM
    {
        public byte[] Priority { get; set; }
        public string[] Status { get; set; }
        public int[] Project { get; set; }
        public string[] AssignedTo { get; set; }
        public string CreateBy { get; set; }
        public int[] Client { get; set; }
    }
}