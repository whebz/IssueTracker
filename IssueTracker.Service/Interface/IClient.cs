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
    }
}
