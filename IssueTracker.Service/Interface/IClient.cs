using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{
    public interface IClient
    {
        IEnumerable<Model.Client> GetList();
        Model.Client GetById(int clientId);
        string Add(Model.Client data);
        string Update(Model.Client data);
        string Delete(int clientId);

        IEnumerable<Model.ViewModel.ClientProjectViewModel> GetClientProjectList(int clientId);
        Model.ViewModel.ClientProjectViewModel GetItem(Model.ClientProject data);

        string AddClientProject(Model.ClientProject data);
        string DeleteClientProject(Model.ClientProject data);
    }
}
