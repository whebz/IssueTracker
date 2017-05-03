using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class Login
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
    }
}