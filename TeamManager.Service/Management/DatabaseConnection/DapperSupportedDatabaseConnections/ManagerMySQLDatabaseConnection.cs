using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System.Data.Common;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections
{
    public class ManagerMySQLDatabaseConnection : ManagerDapperSupportedDatabaseConnection
    {
        public ManagerMySQLDatabaseConnection(string connectionString) : base(connectionString)
        {
        }

        protected override DbConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
