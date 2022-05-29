using MySql.Data.MySqlClient;
using System.Data.Common;

namespace TeamManager.Service.UnitTest.HelperMethods.Database
{
    public class MySqlDatabaseTestHelper : DatabaseTestHelper
    {
        protected override string UserIDToTeamIDTableSQL => @"CREATE TABLE UserID_To_TeamID (
        	                                        ID	INTEGER AUTO_INCREMENT PRIMARY KEY,
	                                                UserID	INTEGER NOT NULL,
	                                                TeamID	INTEGER NOT NULL
	                                                );";

        protected override string TeamsTableSQL => @"CREATE TABLE Teams (
	                                        ID	INTEGER AUTO_INCREMENT PRIMARY KEY,
	                                        Name	varchar(255)  NOT NULL,
	                                        CreationDate	varchar(255),
                                            UNIQUE (Name)
	                                        );";



        protected override string PurposesTableSQL => @"CREATE TABLE Purposes (
	                                            ID	INTEGER AUTO_INCREMENT PRIMARY KEY,
	                                            PurposeText	varchar(255)   NOT NULL,
	                                            UserName	varchar(255)   NOT NULL
	                                            );";

        protected override string ManagersTableSQL => @"CREATE TABLE Managers (
                                                ID	INTEGER AUTO_INCREMENT PRIMARY KEY,
                                                UserName	varchar(255)   NOT NULL,
                                                Password	varchar(255)   NOT NULL,
                                                UNIQUE (UserName)
                                                );";

        protected override string UserTableSQL => @"CREATE TABLE Users (
                                            ID	INTEGER AUTO_INCREMENT PRIMARY KEY,
                                            Name	varchar(255)  ,
                                            Surname	varchar(255)  ,
                                            CreationDate	varchar(255)  ,
                                            PhoneNumber	varchar(255),
                                            Title	varchar(255)  
                                            );";

        protected override DbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        protected override string GetConnectionString()
        {
            var connectionString = $"Server = 127.0.0.1; Uid = root; Pwd = pass; database={dbName}";
            return connectionString;
        }

        protected override void DeleteDBIfExists()
        {
            string connectionString = $"Server = 127.0.0.1; Uid = root; Pwd = pass";

            using (DbConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string createDBQuery = $"DROP DATABASE IF EXISTS `{dbName}`;";
                var cmd = new MySqlCommand(createDBQuery, conn as MySqlConnection);
                cmd.ExecuteNonQuery();
            }
        }

        protected override void RunSQL(string sqlCommand)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(sqlCommand, conn as MySqlConnection);
                cmd.ExecuteNonQuery();
            }
        }

        protected override void CreateDB()
        {
            string connectionString = $"Server = 127.0.0.1; Uid = root; Pwd = pass";

            using (DbConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string createDBQuery = $"CREATE DATABASE IF NOT EXISTS `{dbName}`;";
                var cmd = new MySqlCommand(createDBQuery, conn as MySqlConnection);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
