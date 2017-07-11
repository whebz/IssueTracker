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
    public class Issue : Interface.IIssue
    {
        private string _cnstring;

        #region ctor
        public Issue(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion

        public string Add(Model.Issue data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("IssueAdd", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }

        public string AssignTo(int id, string userId, string description)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute(
                        "IssueAssignTo",
                        new { @Id = id, @AssignedTo = userId, @Description = description },
                        commandType: CommandType.StoredProcedure
                    );
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public string Close(int id)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("IssueSetStatusClosed", new { @Id = id }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public string Delete(int id)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("IssueDelete", new { @Id = id }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public Model.Issue GetById(int id)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<IssueViewModel>(
                    "IssueById",
                    new { @id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<IssueViewModel> GetLatest()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<IssueViewModel>(
                    "IssueLatest",
                    commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<IssueViewModel> GetList(string accountId, string filter)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<IssueViewModel>(
                    "IssueList",
                    new { @AccountId = accountId, @filter = filter },
                    commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<IssueStatusStat> GetStatusStats()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<IssueStatusStat>(
                    "IssueStatistics",
                    commandType: CommandType.StoredProcedure);
        }

        public string Update(Model.Issue data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("IssueUpdate", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }
    }
}
