using TeamManager.Service.Models;
using TeamManager.Service.WizardSection.Database;

namespace TeamManager.Service.WizardSection
{
    public class LoginPageService
    {
        IWizardDatabaseConnection databaseConnection;

        public LoginPageService(IWizardDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public bool CheckIfManagerExists(Manager manager)
        {
            Manager managerFromDB = databaseConnection.GetManager(manager.UserName);

            if (managerFromDB == null || managerFromDB.Password != manager.Password)
                return false;

            return true;
        }
    }
}
