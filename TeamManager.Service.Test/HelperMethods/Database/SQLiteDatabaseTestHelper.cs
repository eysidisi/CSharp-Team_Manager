using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace TeamManager.Service.UnitTest.HelperMethods.Database
{
    public class SQLiteDatabaseTestHelper : DatabaseTestHelperBase
    {
        protected override string UserIDToTeamIDTableSQL => @"CREATE TABLE 'UserID_To_TeamID' (
        	                                        'ID'	INTEGER NOT NULL UNIQUE,
	                                                'UserID'	INTEGER NOT NULL,
	                                                'TeamID'	INTEGER NOT NULL,
	                                                PRIMARY KEY('ID' AUTOINCREMENT));";

        protected override string TeamsTableSQL => @"CREATE TABLE 'Teams' (
	                                        'ID'	INTEGER NOT NULL UNIQUE,
	                                        'Name'	TEXT NOT NULL UNIQUE,
	                                        'CreationDate'	TEXT,
	                                        PRIMARY KEY('ID' AUTOINCREMENT));";



        protected override string PurposesTableSQL => @"CREATE TABLE 'Purposes' (
	                                            'ID'	INTEGER UNIQUE,
	                                            'PurposeText'	TEXT NOT NULL,
	                                            'UserName'	TEXT NOT NULL,
	                                            PRIMARY KEY('ID' AUTOINCREMENT));";

        protected override string ManagersTableSQL => @"CREATE TABLE 'Managers' (
                                                'ID'	INTEGER NOT NULL UNIQUE,
                                                'UserName'	TEXT NOT NULL UNIQUE,
                                                'Password'	TEXT NOT NULL,
                                                PRIMARY KEY('ID' AUTOINCREMENT));";

        protected override string UserTableSQL => @"CREATE TABLE 'Users' (
                                            'ID'	INTEGER NOT NULL UNIQUE,
                                            'Name'	TEXT,
                                            'Surname'	TEXT,
                                            'CreationDate'	TEXT,
                                            'PhoneNumber'	TEXT,
                                            'Title'	TEXT,
                                            PRIMARY KEY('ID' AUTOINCREMENT));";

        protected override void RunSQL(string sqlCommand)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                var command = new SQLiteCommand(sqlCommand, conn as SQLiteConnection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        protected override void DeleteDBIfExists()
        {
            string dbPath = $@"{Directory.GetCurrentDirectory()}\{dbName}.db";

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
        }

        protected override string GetConnectionString()
        {
            string dbPath = $@"{Directory.GetCurrentDirectory()}\{dbName}.db";

            string connectionString = $"Data Source ={dbPath}; Version = 3";

            return connectionString;
        }

        protected override DbConnection CreateConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        protected override void CreateDB()
        {
            string dbPath = $@"{Directory.GetCurrentDirectory()}\{dbName}.db";
            SQLiteConnection.CreateFile(dbPath);
        }
    }
}