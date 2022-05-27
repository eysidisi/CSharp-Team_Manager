using Dapper.Contrib.Extensions;
using System.Data.SQLite;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.DatabaseConnection
{
    public class WizardSQLiteDatabaseConnection : IWizardDatabaseConnection
    {
        readonly string connectionString;
        public WizardSQLiteDatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Manager> GetManagers()
        {
            using (var cnn = new SQLiteConnection(connectionString))
            {
                return cnn.GetAll<Manager>().ToList();
            }
        }

        public void SavePurpose(Purpose purpose)
        {
            using (var cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(purpose);
            }
        }
    }
}
