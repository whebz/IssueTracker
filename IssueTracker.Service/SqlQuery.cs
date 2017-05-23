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
using IssueTracker.Model.ViewModel;

namespace IssueTracker.Service
{
    public class SqlQuery
    {
        private string _cnstring;

        public SqlQuery(string cn)
        {
            _cnstring = Connection.Get(cn); ;
        }

        public IEnumerable<TM> GetList<TM>(string stored)
        {
            using (var sql = new SqlConnection(_cnstring))
            {
                return sql.Query<TM>(stored, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public IEnumerable<TM> GetList<TM>(string stored, DynamicParameters param)
        {
            using (var sql = new SqlConnection(_cnstring))
            {
                return sql.Query<TM>(stored, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
