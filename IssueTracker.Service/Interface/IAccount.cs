using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Service.Interface
{
    public interface IAccount
    {
        string Add(Model.Account account);

        string Update(Model.Account account);

        string Delete(string accountId);

        IEnumerable<Model.ViewModel.AccountViewModel> GetList();

        Model.Account GetById(string accountId);

        Model.ViewModel.AccountViewModel Authentication(string accountId, string password);

        IEnumerable<Model.AccountType> GetAccountTypeList();

        IEnumerable<Model.ViewModel.AccountByType> GetAccountSummary();

        string ChangePasword(string accountid, string password);
    }
}
