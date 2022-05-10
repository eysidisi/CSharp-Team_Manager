using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Wizard.Database
{
    public class WizardDatabaseManagerSQLite : WizardDatabaseManager
    {
        public WizardDatabaseManagerSQLite(string connString):base(connString)
        {

        }

        protected override void SetConnection()
        {
            cnn = new SQLiteConnection(connString);
        }
    }
}
