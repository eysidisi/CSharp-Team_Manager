﻿using System.Data.SQLite;
using System.IO;

namespace TeamManager.Service.Test.SQliteDB
{
    public class HelperMethods
    {
        readonly public static string validManagerName = "validUserName";
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

                string userTableSQL = @"CREATE TABLE 'Users' (
                                    'UserName'  TEXT NOT NULL UNIQUE,
                                    'Password'  TEXT NOT NULL,
                                 'ID'    INTEGER NOT NULL UNIQUE,
                                    'Name'  TEXT,
                                 'Surname'   TEXT,
                                 'CreationDate'  TEXT,
                                 'PhoneNumber'   INTEGER,
                                 'Title' INTEGER,
                                 PRIMARY KEY('ID' AUTOINCREMENT));";

                SQLiteCommand command = new SQLiteCommand(userTableSQL, conn);
                command.ExecuteNonQuery();
                command.Dispose();

                string managersTableSQL = @"CREATE TABLE 'Managers' (
	                                    'UserName'	TEXT NOT NULL UNIQUE,
	                                    'Password'	TEXT NOT NULL,
	                                    'ID'	INTEGER NOT NULL UNIQUE,
	                                    PRIMARY KEY('ID' AUTOINCREMENT));";
                command = new SQLiteCommand(managersTableSQL, conn);
                command.ExecuteNonQuery();
                command.Dispose();


                string purposesTableSQL = @"CREATE TABLE 'Purposes' (
	                                    'ID'	INTEGER UNIQUE,
	                                    'PurposeText'	TEXT NOT NULL,
	                                    'UserName'	TEXT NOT NULL,
	                                    PRIMARY KEY('ID' AUTOINCREMENT));";

                command = new SQLiteCommand(purposesTableSQL, conn);
                command.ExecuteNonQuery();
                command.Dispose();

                string insertUserSQL = "INSERT INTO Managers ( UserName, Password) VALUES (?,?)";
                SQLiteCommand insertSQLCommand = new SQLiteCommand(insertUserSQL, conn);
                insertSQLCommand.Parameters.Add(new SQLiteParameter("UserName", validManagerName));
                insertSQLCommand.Parameters.Add(new SQLiteParameter("Password", validPassword));
                insertSQLCommand.ExecuteNonQuery();
                insertSQLCommand.Dispose();
            }

            return dbPath;
        }
    }
}