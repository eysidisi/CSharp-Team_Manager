using System.Data.SQLite;
using System.IO;

namespace TeamManager.Service.Test.SQliteDB
{
    public class HelperMethods
    {
        readonly public static string validUserName = "validUserName";
        readonly public static string validPassword = "validPassword";

        static int dbNumber = 0;
        static object syncObj = new object();

        public void DeleteDB(string dbFilePath)
        {
            if (File.Exists(dbFilePath))
            {
                File.Delete(dbFilePath);
            }
        }

        /// <summary>
        /// Creates a test DB, adds some data and returns connection string
        /// </summary>
        /// <returns></returns>
        public string CreateTestDB_ReturnFilePath()
        {
            int currentDbNum;
            lock (syncObj)
            {
                currentDbNum = dbNumber++;
            }

            string dbPath = $@"{ Directory.GetCurrentDirectory() }\TestDB{currentDbNum}.db";

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                conn.Open();

                string sql = " CREATE TABLE 'Users' ( 'UserName' TEXT NOT NULL UNIQUE, 'Password' TEXT, 'ID' INTEGER NOT NULL UNIQUE, PRIMARY KEY('ID' AUTOINCREMENT) )";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                command.Dispose();


                sql = "CREATE TABLE 'Purposes'('ID' INTEGER UNIQUE, 'PurposeText' TEXT NOT NULL, 'UserName' TEXT NOT NULL, PRIMARY KEY('ID' AUTOINCREMENT))";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                command.Dispose();

                sql = "INSERT INTO Users ( UserName, Password) VALUES (?,?)";
                SQLiteCommand insertSQL = new SQLiteCommand(sql, conn);
                insertSQL.Parameters.Add(new SQLiteParameter("UserName", validUserName));
                insertSQL.Parameters.Add(new SQLiteParameter("Password", validPassword));
                insertSQL.ExecuteNonQuery();
                insertSQL.Dispose();
            }

            return dbPath;
        }
    }
}