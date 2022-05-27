using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service.Wizard.DatabaseConnection;

namespace TeamManager.Service.Wizard.DatabaseManagers
{
    public class WizardMySQLDatabaseController : WizardDatabaseController
    {
        public WizardMySQLDatabaseController(string connString) : base(connString)
        {
        }

        protected override IWizardDatabaseConnection CreateConnection()
        {
            return new WizardMySQLDatabaseConnection(connString);
        }
    }
}
