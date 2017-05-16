using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class AccountEditorVM
    {
        public string ActionState { get; set; }
        public Model.Account Account { get; set; }
    }
}