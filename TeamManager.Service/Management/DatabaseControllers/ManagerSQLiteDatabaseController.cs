using TeamManager.Service.Management.DatabaseConnection;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;

namespace TeamManager.Service.Management.DatabaseControllers
{
    public class ManagerSQLiteDatabaseController : ManagerDatabaseController
    {
        public ManagerSQLiteDatabaseController(string connString) : base(connString)
        {
            this.connString = connString;
        }

        protected override IManagerDatabaseConnection CreateConnection()
        {
            return new ManagerSQLiteDatabaseConnection(connString);
        }
    }
}
