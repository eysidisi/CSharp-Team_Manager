using TeamManager.Service.Wizard.DatabaseController;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard
{
    public class LoginPageService
    {
        readonly WizardDatabaseController databaseConnection;

        public LoginPageService(WizardDatabaseController databaseController)
        {
            this.databaseConnection = databaseController;
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
