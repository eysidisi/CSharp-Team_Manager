using Dapper;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Database
{
    public class SQLiteDataAccess : IDatabaseConnection
    {
        string connString;

        public SQLiteDataAccess(string connString)
        {
            this.connString = connString;
        }

        public bool CheckIfManagerExists(Manager manager)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"SELECT * From Managers where UserName = '{manager.UserName}' and Password = '{manager.Password}'";
                var output = cnn.Query<User>(query);

                if (output != null && output.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SavePurpose(Purpose purpose)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"INSERT INTO Purposes (UserName,PurposeText) VALUES('{purpose.UserName}','{purpose.PurposeText}');";
                int output = cnn.Execute(query);

                if (output != 1)
                    throw new Exception("Can't add purpose!");
            }
        }

        public List<Purpose> GetPurposes(string userName)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"SELECT * From Purposes where UserName = '{userName}'";
                var output = cnn.Query<Purpose>(query);
                
                if (output == null || output.Count() == 0)
                {
                    throw new Exception("Can't find purpose related to that manager!");
                }

                return output.ToList();
            }
        }

        public Manager GetManager(string userName)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"SELECT * From Managers where UserName = '{userName}'";
                var output = cnn.Query<Manager>(query);
                
                if (output == null || output.Count() == 0)
                {
                    throw new Exception("Can't find manager!");
                }

                return output.First();
            }
        }
    }
}
