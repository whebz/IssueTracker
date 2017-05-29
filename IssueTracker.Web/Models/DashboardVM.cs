using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class DashboardVM
    {
        public int Assigned { get; set; }
        public int NotAssigned { get; set; }
        public int Closed { get; set; }
        public int Cancelled { get; set; }

        public DashboardVM(List<Model.ViewModel.IssueStatusStat> stats)
        {
            stats.ForEach(c =>
            {
                switch (c.Name)
                {
                    case "N":
                        NotAssigned = c.Count;
                        break;
                    case "A":
                        Assigned = c.Count;
                        break;
                    case "S":
                        Cancelled = c.Count;
                        break;
                    case "C":
                        Closed = c.Count;
                        break;
                }
            });
        }
    }
}