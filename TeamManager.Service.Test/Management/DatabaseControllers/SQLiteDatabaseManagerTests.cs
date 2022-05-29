﻿using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseControllers
{
    public class SQLitedatabaseControllerTests : DatabaseControllerTestsBaseClass
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseHelperMethods()
        {
            return new SQLiteDatabaseTestHelper();
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }
    }
}
