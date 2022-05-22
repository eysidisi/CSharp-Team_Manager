using Dapper.Contrib.Extensions;
using System.Data;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.Database
{
    public abstract class WizardDatabaseManager : IWizardDatabaseConnection
    {
        protected string connString;
        protected IDbConnection cnn;

        public WizardDatabaseManager(string connString)
        {
            this.connString = connString;
            SetConnection();
        }

        protected abstract void SetConnection();

        public void SavePurpose(Purpose purpose)
        {
            cnn.Insert(purpose);
        }

        public List<Manager> GetManagers()
        {
            return cnn.GetAll<Manager>().ToList();
        }
    }
}
