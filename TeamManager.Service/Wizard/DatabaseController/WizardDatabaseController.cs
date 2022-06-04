using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.DatabaseConnection;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.DatabaseController
{
    // Used for database operations' optimization
    public class WizardDatabaseController
    {
        protected string connString;
        protected IWizardDatabaseConnection connection;

        public WizardDatabaseController(IWizardDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public virtual void SavePurpose(Purpose purpose)
        {
            connection.SavePurpose(purpose);
        }

        public virtual List<Manager> GetManagers()
        {
            return connection.GetManagers();
        }
    }
}
