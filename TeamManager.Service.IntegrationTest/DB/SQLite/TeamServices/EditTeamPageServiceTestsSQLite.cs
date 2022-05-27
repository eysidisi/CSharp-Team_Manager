using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class EditTeamPageServiceTestsSQLite : EditTeamPageServiceTests
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new SQLiteDatabaseTestHelper();
        }
    }
}
