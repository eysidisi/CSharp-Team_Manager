using TeamManager.Service.Wizard.DatabaseConnection;

namespace TeamManager.Service.Wizard.DatabaseControllers
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
