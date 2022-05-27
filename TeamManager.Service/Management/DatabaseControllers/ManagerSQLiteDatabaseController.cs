using TeamManager.Service.Management.DatabaseConnection;

namespace TeamManager.Service.Management.DatabaseManagers
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
