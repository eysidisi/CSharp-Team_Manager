using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.Database
{
    public class ManagementSQLiteConnetion : ManagementDatabaseManager
    {
        public ManagementSQLiteConnetion(string connString) : base(connString)
        {
            this.connString = connString;
        }

        protected override void CreateConnection()
        {
            dbConnection = new SQLiteConnection(connString);
        }
    }
}
