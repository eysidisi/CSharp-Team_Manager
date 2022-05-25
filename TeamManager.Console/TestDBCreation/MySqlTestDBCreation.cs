using MySql.Data.MySqlClient;
using System.Data.Common;

namespace TeamManager.Console.TestDBCreation
{
    public class MySqlTestDBCreation : TestDBCreationBase
    {
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}
