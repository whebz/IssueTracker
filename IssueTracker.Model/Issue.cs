using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Model
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string AssignTo { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProjectId { get; set; }
        public string IssueType { get; set; }
        public string IssueStatus { get; set; }
        public byte PriorityId { get; set; }
    }
}
