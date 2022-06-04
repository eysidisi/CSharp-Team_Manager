﻿using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.IntegrationTest.DB.UserServices;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.MySQL.UserServices
{
    public class UserDetailsPageServiceTestsMySQL : UserDetailsPageServiceTests
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            var connection = new ManagerMySQLDatabaseConnection(connectionString);
            return new ManagerDatabaseController(connection);
        }

        protected override DatabaseTestHelper CreateDatabaseHelperMethods()
        {
            return new MySqlDatabaseTestHelper();
        }
    }
}
