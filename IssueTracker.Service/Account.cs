using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Model;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Dapper;

namespace IssueTracker.Service
{
    public class Account : Interface.IAccount
    {

        private string _cnstring;

        #region ctor
        public Account(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion

        public string Add(Model.Account account)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("Account_Insert", account, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }

        public string Delete(string accountId)
        {
            throw new NotImplementedException();
        }

        public Model.Account GetById(string accountId)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.ViewModel.AccountViewModel>(
                    "AccountWithFKDescriptionById", 
                    new { @AccountId = accountId },
                    commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
        }

        public Model.Account GetByIdAndPassword(string accountId, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.ViewModel.AccountViewModel> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.ViewModel.AccountViewModel>("AccountWithFKDescription", commandType: CommandType.StoredProcedure);
        }

        public string Update(Model.Account account)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("Account_Update", account, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }

        public IEnumerable<AccountType> GetAccountTypeList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.AccountType>("SELECT * FROM AccountType WIH (NOLOCK)", commandType: CommandType.Text);
        }
    }
}
