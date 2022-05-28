﻿using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.IntegrationTest.DB.TeamServices;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.MySQL.TeamServices
{
    public class TeamDetailsPageServiceTestsMySQL : TeamDetailsPageServiceTests
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            return new ManagerMySQLDatabaseController(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new MySqlDatabaseTestHelper();
        }
    }
}
