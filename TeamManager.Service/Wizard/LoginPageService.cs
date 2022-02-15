using TeamManager.Service.Models;
using TeamManager.Service.Wizard.Database;

namespace TeamManager.Service.Wizard
{
    public class LoginPageService
    {
        IWizardDatabaseConnection databaseConnection;

        public LoginPageService(IWizardDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Manager GetManager(Manager managerToFind)
        {
            var allManagers = databaseConnection.GetManagers();

            Manager? manager = allManagers.Find(m => m.UserName == managerToFind.UserName && m.Password == managerToFind.Password);

            if (manager == null)
            {
                throw new ArgumentException("Can't find the manager! Check the entered info!");
            }

            return manager;
        }
    }
}
