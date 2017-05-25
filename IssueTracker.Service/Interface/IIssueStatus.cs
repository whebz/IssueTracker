using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{
    public interface IIssueStatus
    {
        IEnumerable<Model.IssueStatus> GetList();

        string Add(Model.IssueStatus data);

        string Update(Model.IssueStatus data);

        string Delete(string id);

        Model.Account GetById(string id);
    }
}
