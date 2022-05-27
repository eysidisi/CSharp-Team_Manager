using TeamManager.Service.Management.DatabaseConnection;

namespace TeamManager.Service.Management.DatabaseManagers
{
    public class ManagerMySQLDatabaseController : ManagerDatabaseController
    {
        public ManagerMySQLDatabaseController(string connString) : base(connString)
        {
        }

        protected override IManagerDatabaseConnection CreateConnection()
        {
            return new ManagerMySQLDatabaseConnection(connString);
        }
    }
}