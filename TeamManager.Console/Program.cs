using TeamManager.Console.TestDBCreation;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Console
{
    internal class Program
    {
        /// <summary>
        /// This project is just used for creating demo DBs with different sizes
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DatabaseTestHelperBase helperMethods = new MySqlDatabaseTestHelper();
            var mySQLConnectionStringSmall = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_small_db");
            var mySQLConnectionStringMedium = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_medium_db");
            var mySQLConnectionStringLarge = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_large_db");

            TestDBCreationBase testDBCreation = new MySqlTestDBCreation();
            testDBCreation.CreateDB(mySQLConnectionStringSmall, numOfManagers: 100, numOfUsers: 100, numberOfTeams: 1);
            testDBCreation.CreateDB(mySQLConnectionStringMedium, numOfManagers: 1000, numOfUsers: 1000, numberOfTeams: 10);
            testDBCreation.CreateDB(mySQLConnectionStringLarge, numOfManagers: 10000, numOfUsers: 100000, numberOfTeams: 100);

            helperMethods = new SQLiteDatabaseTestHelper();
            var sqliteConnectionStringSmall = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_small_db");
            var sqliteConnectionStringMedium = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_medium_db");
            var sqliteonnectionStringLarge = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString("test_large_db");

            testDBCreation = new SQLiteTestDBCreation();
            testDBCreation.CreateDB(sqliteConnectionStringSmall, numOfManagers: 100, numOfUsers: 100, numberOfTeams: 1);
            testDBCreation.CreateDB(sqliteConnectionStringMedium, numOfManagers: 1000, numOfUsers: 1000, numberOfTeams: 10);
            testDBCreation.CreateDB(sqliteonnectionStringLarge, numOfManagers: 10000, numOfUsers: 100000, numberOfTeams: 100);
        }
    }
}
