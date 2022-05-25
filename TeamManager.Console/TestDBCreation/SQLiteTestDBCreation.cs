using System.Data.Common;
using System.Data.SQLite;

namespace TeamManager.Console.TestDBCreation
{
    public class SQLiteTestDBCreation : TestDBCreationBase
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
