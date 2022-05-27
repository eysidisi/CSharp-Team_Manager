using TeamManager.Service.Wizard.DatabaseConnection;

namespace TeamManager.Service.Wizard.DatabaseControllers
{
    public class WizardMySQLDatabaseController : WizardDatabaseController
    {
        public WizardMySQLDatabaseController(string connString) : base(connString)
        {
        }

        protected override IWizardDatabaseConnection CreateConnection()
        {
            return new WizardMySQLDatabaseConnection(connString);
        }
    }
}
