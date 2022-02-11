using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Wizard.Database
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

        public List<Purpose> GetPurposes(string userName)
        {
            string query = $"SELECT * From Purposes where UserName = @UserName";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var output = cnn.Query<Purpose>(query, new Purpose() { UserName = userName });
                return output.ToList();
            }
        }

        public Manager GetManager(string userName)
        {
            string query = $"SELECT * From Managers where UserName = @UserName";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var output = cnn.Query<Manager>(query, new Manager() { UserName = userName });
                return output.FirstOrDefault();
            }
        }
    }
}
