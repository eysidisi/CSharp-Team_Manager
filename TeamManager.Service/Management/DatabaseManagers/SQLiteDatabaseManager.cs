using TeamManager.Service.Management.DatabaseConnection;

namespace TeamManager.Service.Management.DatabaseManagers
{
    public class SQLiteDatabaseManager : DatabaseManager
    {
        public SQLiteDatabaseManager(string connString) : base(connString)
        {
            this.connString = connString;
        }

        protected override IDatabaseConnection CreateConnection()
        {
            return new SQLiteDatabaseConnection(connString);
        }
    }
}
