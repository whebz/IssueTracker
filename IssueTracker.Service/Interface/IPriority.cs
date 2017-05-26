using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{
    public interface IPriority
    {
        IEnumerable<Model.Priority> GetList();
        Model.Priority GetById(byte id);
        string Add(Model.Priority data);
        string Update(Model.Priority data);
        string Delete(byte id);
    }
}
