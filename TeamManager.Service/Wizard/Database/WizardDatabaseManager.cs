using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Models;

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
