using System.Data.SQLite;
using TeamManager.Service.Wizard.DatabaseConnection;

namespace TeamManager.Service.Wizard.Database
{
    public class WizardSQLiteDatabaseController : WizardDatabaseController
    {
        public WizardSQLiteDatabaseController(string connString) : base(connString)
        {

        }

        protected override IWizardDatabaseConnection CreateConnection()
        {
            return new WizardSQLiteDatabaseConnection(connString);
        }
    }
}
