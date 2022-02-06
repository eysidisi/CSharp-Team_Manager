using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;

namespace TeamManager.Service
{
    public class SQLiteDataAccess : IDatabaseConnection
    {
        string connString;

        public SQLiteDataAccess(string connString)
        {
            this.connString = connString;
        }

        public bool CheckIfUserExists(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"SELECT * From Users where UserName = '{user.UserName}' and Password = '{user.Password}'";
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
                return output.ToList();
            }
        }

        public User GetUser(string userName)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                string query = $"SELECT * From Users where UserName = '{userName}'";
                var output = cnn.Query<User>(query);
                return output.First();
            }
        }
    }
}
