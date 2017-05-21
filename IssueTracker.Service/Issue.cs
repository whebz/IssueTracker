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

        public IEnumerable<IssueViewModel> GetList(string accountId, string filter)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<IssueViewModel>(
                    "IssueList",
                    new { @AccountId = accountId, @filter = filter },
                    commandType: CommandType.StoredProcedure);
        }

        public IssueStatusSummary GetStatusStats(string accountId, string filter)
        {
            List<IssueStatusStat> list;
            var stat = new IssueStatusSummary();
            using (var sql = new SqlConnection(_cnstring))
                list = sql.Query<IssueStatusStat>(
                    "IssueList",
                    new { @AccountId = accountId, @filter = filter },
                    commandType: CommandType.StoredProcedure).ToList();
            if (list?.Count > 0)
            {
                list.ForEach(x =>
                {
                    switch(x.Name)
                    {
                        case "Assigned":
                            stat.Assigned = x.Count;
                            break;
                        case "Closed":
                            stat.Closed = x.Count;
                            break;
                        case "NotAssigned":
                            stat.NotAssigned = x.Count;
                            break;
                        case "OnProgress":
                            stat.OnProgress = x.Count;
                            break;
                        case "Suspended":
                            stat.Suspended = x.Count;
                            break;
                    }
                });
            }
            return stat;
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
