using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{
    public interface IProject
    {
        string Add(Model.Project data);
        string Update(Model.Project data);
        string Delete(int projectId);

        IEnumerable<Model.ViewModel.ClientProjectViewModel> GetProjectClientList(int projectId);
        IEnumerable<Model.Project> GetList();
    }
}
