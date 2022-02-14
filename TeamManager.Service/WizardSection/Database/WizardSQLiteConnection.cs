using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.WizardSection.Database
{
    public class WizardSQLiteConnection : IWizardDatabaseConnection
    {
        string connString;

        public WizardSQLiteConnection(string connString)
        {
            this.connString = connString;
        }

        public void SavePurpose(Purpose purpose)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(purpose);
            }
        }

        public List<Manager> GetManagers()
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
               return cnn.GetAll<Manager>().ToList();
            }
        }
    }
}
