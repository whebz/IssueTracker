using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Web.Models
{
    public class CommonFormVM : Model.Base.INSModel
    {
        public string HeaderText { get; set; }
        public int idx { get; set; }
        public string Mode { get; set; }

        public CommonFormVM (int indx)
        {
            idx = indx;
            Mode = "I";
        }
        public CommonFormVM(int indx, Model.Base.INSModel m)
        {
            idx = indx;
            Mode = "U";
            Id = m.Id;
            Name = m.Name;
            Suspended = m.Suspended;
        }
    }
}