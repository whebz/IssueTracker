using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Model;

namespace IssueTracker.Service
{
    public class Account : Interface.IAccount
    {

        #region ctor
        #endregion

        public string Add(Model.Account account)
        {
            throw new NotImplementedException();
        }

        public string Delete(string accountId)
        {
            throw new NotImplementedException();
        }

        public Model.Account GetById(string accountId)
        {
            throw new NotImplementedException();
        }

        public Model.Account GetByIdAndPassword(string accountId, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Account> GetList()
        {
            throw new NotImplementedException();
        }

        public string Update(Model.Account account)
        {
            throw new NotImplementedException();
        }
    }
}
