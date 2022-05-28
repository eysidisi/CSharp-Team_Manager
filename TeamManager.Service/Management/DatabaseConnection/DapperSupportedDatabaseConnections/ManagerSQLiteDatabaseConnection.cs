using System.Data.Common;
using System.Data.SQLite;

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
