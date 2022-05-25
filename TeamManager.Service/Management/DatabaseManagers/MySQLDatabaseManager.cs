using TeamManager.Service.Management.DatabaseConnection;

namespace TeamManager.Service.Management.DatabaseManagers
{
    public class MySQLDatabaseManager : DatabaseManager
    {
        public MySQLDatabaseManager(string connString) : base(connString)
        {
        }

        protected override IDatabaseConnection CreateConnection()
        {
            return new MySQLDatabaseConnection(connString);
        }
    }
}