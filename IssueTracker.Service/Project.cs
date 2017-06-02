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
    public class Project : Interface.IProject
    {        
        private string _cnstring;

        #region ctor
        public Project(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion

        public string Add(Model.Project data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ProjectAdd", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }

        public string Delete(int projectId)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ProjecDelete", new { @Id = projectId }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public IEnumerable<Model.Project> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.Project>("ProjectList", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Model.Project> GetList(int clientId)
        {
            var p = new DynamicParameters();
            p.Add("ClientId", clientId);
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.Project>("ProjectList", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<ClientProjectViewModel> GetProjectClientList(int projectId)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<ClientProjectViewModel>(
                        "ClientProjectWithNames",
                        new { @ProjectId = projectId },
                        commandType: CommandType.StoredProcedure
                );
        }

        public string Update(Model.Project data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ProjectUpdate", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }
    }
}
