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

        private string _cname;
        private string _cnstring
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[_cname].ConnectionString;
            }
        }
        #region ctor
        public Account(string cn)
        {
            _cname = cn;
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
            throw new NotImplementedException();
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
    }
}
