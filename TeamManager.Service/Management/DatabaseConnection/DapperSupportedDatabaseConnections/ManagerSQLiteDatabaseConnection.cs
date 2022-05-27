using Dapper.Contrib.Extensions;
using System.Data.Common;
using System.Data.SQLite;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections
{
    public class ManagerSQLiteDatabaseConnection : ManagerDapperSupportedDatabaseConnection
    {
        public ManagerSQLiteDatabaseConnection(string connectionString) : base(connectionString)
        {
        }

        protected override DbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
