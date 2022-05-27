using System;
using System.Data;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB
{
    public abstract class IntegrationTests
    {
        protected string connString;
        protected ManagerDatabaseController databaseController;
        DatabaseTestHelperBase databaseTestHelperMethods;

        public IntegrationTests()
        {
            databaseTestHelperMethods = CreateDatabaseHelperMethods();
            connString = databaseTestHelperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            databaseController = CreateDatabaseController(connString);
        }

        protected abstract DatabaseTestHelperBase CreateDatabaseHelperMethods();

        protected abstract IDbConnection CreateConnection(string connectionString);

        protected abstract ManagerDatabaseController CreateDatabaseController(string connectionString);
    }
}