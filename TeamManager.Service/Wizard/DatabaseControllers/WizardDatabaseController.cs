using Dapper.Contrib.Extensions;
using System.Data;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.DatabaseConnection;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.Database
{
    // Used for database operations' optimization
    public abstract class WizardDatabaseController
    {
        protected string connString;
        protected IWizardDatabaseConnection cnn;

        public WizardDatabaseController(string connString)
        {
            this.connString = connString;
            cnn = CreateConnection();
        }

        protected abstract IWizardDatabaseConnection CreateConnection();

        public virtual void SavePurpose(Purpose purpose)
        {
            cnn.SavePurpose(purpose);
        }

        public virtual List<Manager> GetManagers()
        {
            return cnn.GetManagers();
        }
    }
}
