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
    public class Client : Interface.IClient
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
        public Client(string cn)
        {
            _cname = cn;
        }
        #endregion

        public IEnumerable<Model.Client> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.Client>("SELECT * FROM Client", commandType: CommandType.Text);
        }

        public Model.Client GetById(int clientId)
        {
            return null;
        }

        public string Add(Model.Client data)
        {
            return null;
        }
        public string Update(Model.Client data)
        {
            return null;
        }

        public string Delete(int clientId)
        {
            return null;
        }
    }
}
