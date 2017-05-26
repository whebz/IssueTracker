using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IssueTracker.Web.Models
{
    public class IssueFormVM
    {
        public Model.Issue Issue { get; set; }
        public string HeaderText { get; set; }
        public string Author { get; set; }
    }
}