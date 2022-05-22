using System.Data.SQLite;

namespace TeamManager.Service.Wizard.Database
{
    public class WizardDatabaseManagerSQLite : WizardDatabaseManager
    {
        public WizardDatabaseManagerSQLite(string connString) : base(connString)
        {

        }

        protected override void SetConnection()
        {
            cnn = new SQLiteConnection(connString);
        }
    }
}
