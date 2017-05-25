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
    public class IssueStatus : Interface.IIssueStatus
    {
        private string _cnstring;

        #region ctor
        public IssueStatus(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion

        public string Add(Model.IssueStatus data)
        {
            throw new NotImplementedException();
        }

        public string Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Model.Account GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.IssueStatus> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.IssueStatus>("IssueStatusGetList", commandType: CommandType.StoredProcedure);
        }

        public string Update(Model.IssueStatus data)
        {
            throw new NotImplementedException();
        }
    }
}
