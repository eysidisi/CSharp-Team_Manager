using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.DatabaseConnection
{
    public class WizardMySQLDatabaseConnection : IWizardDatabaseConnection
    {
        readonly string connectionString;
        public WizardMySQLDatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Manager> GetManagers()
        {
            using (var cnn = new MySqlConnection(connectionString))
            {
                return cnn.GetAll<Manager>().ToList();
            }
        }

        public void SavePurpose(Purpose purpose)
        {
            using (var cnn = new MySqlConnection(connectionString))
            {
                cnn.Insert(purpose);
            }
        }
    }
}
