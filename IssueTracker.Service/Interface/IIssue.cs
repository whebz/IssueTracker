﻿using IssueTracker.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{    
    public interface IIssue
    {
        IEnumerable<IssueViewModel> GetList(string accountId, string filter);
        IssueStatusSummary GetStatusStats(string accountId, string filter);
        string Add(Model.Issue data);
        string Update(Model.Issue data);
        string Delete(int id);
        string Close(int id);
        string AssignTo(int id, string userId);

        Model.Issue GetById(int id);
    }
}
