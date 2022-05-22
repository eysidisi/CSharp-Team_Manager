using System.Data.SQLite;
using System.IO;

namespace TeamManager.Service.UnitTest.HelperMethods.SQLiteDB
{
    public class SQLiteHelperMethods
    {
        private const string UserIDToTeamIDTableSQL = @"CREATE TABLE 'UserID_To_TeamID' (
        	                                        'ID'	INTEGER NOT NULL UNIQUE,
	                                                'UserID'	INTEGER NOT NULL,
	                                                'TeamID'	INTEGER NOT NULL,
	                                                PRIMARY KEY('ID' AUTOINCREMENT));";

        private const string TeamsTableSQL = @"CREATE TABLE 'Teams' (
	                                        'ID'	INTEGER NOT NULL UNIQUE,
	                                        'Name'	TEXT NOT NULL UNIQUE,
	                                        'CreationDate'	TEXT,
	                                        PRIMARY KEY('ID' AUTOINCREMENT));";

        private const string PurposesTableSQL = @"CREATE TABLE 'Purposes' (
	                                            'ID'	INTEGER UNIQUE,
	                                            'PurposeText'	TEXT NOT NULL,
	                                            'UserName'	TEXT NOT NULL,
	                                            PRIMARY KEY('ID' AUTOINCREMENT));";

        private const string ManagersTableSQL = @"CREATE TABLE 'Managers' (
                                                'ID'	INTEGER NOT NULL UNIQUE,
                                                'UserName'	TEXT NOT NULL UNIQUE,
                                                'Password'	TEXT NOT NULL,
                                                PRIMARY KEY('ID' AUTOINCREMENT));";

        private const string UserTableSQL = @"CREATE TABLE 'Users' (
                                            'ID'	INTEGER NOT NULL UNIQUE,
                                            'Name'	TEXT,
                                            'Surname'	TEXT,
                                            'CreationDate'	TEXT,
                                            'PhoneNumber'	INTEGER,
                                            'Title'	TEXT,
                                            PRIMARY KEY('ID' AUTOINCREMENT));";


        public static readonly string ValidManagerUserName = "validUserName";
        public static readonly string ValidManagerPassword = "validPassword";

        static int NextDBNumber = 0;
        static readonly object syncObj = new object();

        private SQLiteConnection sqliteConnection;

        public void DeleteDBIfExists(string dbFilePath)
        {
            if (File.Exists(dbFilePath))
            {
                File.Delete(dbFilePath);
            }
        }

        /// <summary>
        /// Creates a test DB and returns the connection string
        /// </summary>
        /// <returns></returns>
        public string CreateEmptyTestDB_ReturnFilePath()
        {
            int currentDBNum = GetNextDBNumber();

            string dbPath = $@"{Directory.GetCurrentDirectory()}\TestDB_{currentDBNum}.db";

            DeleteDBIfExists(dbPath);

            SQLiteConnection.CreateFile(dbPath);

            using (sqliteConnection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                sqliteConnection.Open();
                AddUserTable();
                AddManagerTable();
                AddPurposeTable();
                AddTeamsTable();
                AddUserIDToTeamIDTable();
            }

            return dbPath;
        }
        private static int GetNextDBNumber()
        {
            int currentDbNum;

            lock (syncObj)
            {
                currentDbNum = NextDBNumber++;
            }

            return currentDbNum;
        }

        private void AddUserIDToTeamIDTable()
        {
            RunSQLInSQLiteConnection(UserIDToTeamIDTableSQL);
        }

        private void AddTeamsTable()
        {
            RunSQLInSQLiteConnection(TeamsTableSQL);
        }

        private void AddPurposeTable()
        {
            RunSQLInSQLiteConnection(PurposesTableSQL);
        }

        private void AddManagerTable()
        {
            RunSQLInSQLiteConnection(ManagersTableSQL);
        }

        private void AddUserTable()
        {
            RunSQLInSQLiteConnection(UserTableSQL);
        }

        private void RunSQLInSQLiteConnection(string sqlCommand)
        {
            var command = new SQLiteCommand(sqlCommand, sqliteConnection);
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}