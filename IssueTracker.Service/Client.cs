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
    public class Client : Interface.IClient
    {
        private string _cnstring;

        #region ctor
        public Client(string cn)
        {
            _cnstring = Connection.Get(cn);
        }
        #endregion

        public IEnumerable<Model.Client> GetList()
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<Model.Client>("SELECT * FROM Client WIH (NOLOCK)", commandType: CommandType.Text);
        }

        public Model.Client GetById(int clientId)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.QueryFirstOrDefault<Model.Client>("SELECT * FROM Client WIH (NOLOCK) Where Id = @Id", 
                    commandType: CommandType.Text, param: new { @Id = clientId });
        }

        public string Add(Model.Client data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ClientAdd", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

            return null;
        }
        public string Update(Model.Client data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ClientUpdate", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public string Delete(int clientId)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ClientDelete", new { @Id = clientId }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public IEnumerable<ClientProjectViewModel> GetClientProjectList(int clientId)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<ClientProjectViewModel>(
                        "ClientProjectWithNames", 
                        new { @ClientId = clientId }, 
                        commandType: CommandType.StoredProcedure
                );
        }

        public ClientProjectViewModel GetItem(ClientProject data)
        {
            using (var sql = new SqlConnection(_cnstring))
                return sql.Query<ClientProjectViewModel>(
                        "ClientProjectWithNames",
                        data,
                        commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
        }

        public string AddClientProject(ClientProject data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ClientProjectAdd", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }

        public string DeleteClientProject(ClientProject data)
        {
            try
            {
                using (var sql = new SqlConnection(_cnstring))
                    sql.Execute("ClientProjectDelete", data, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
            return null;
        }
    }
}
