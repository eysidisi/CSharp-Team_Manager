using MySql.Data.MySqlClient;
using System.Data.Common;

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
