using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace IssueTracker.Service
{
    public class Priority : Interface.IPriority
    {

        private string _cnstring;

        #region ctor
        public Priority(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion


        public string Add(Model.Priority data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("PriorityUpdate", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public string Delete(byte id)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("PriorityDelete", new { @Id = id }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public Model.Priority GetById(byte id)
        {
            return GetList().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Model.Priority> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.Priority>("PriorityList", commandType: CommandType.StoredProcedure);
        }

        public string Update(Model.Priority data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("PriorityUpdate", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }
    }
}
