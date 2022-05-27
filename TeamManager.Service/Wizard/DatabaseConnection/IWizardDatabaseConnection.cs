using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.Service.Wizard.DatabaseConnection
{
    public interface IWizardDatabaseConnection
    {
        public void SavePurpose(Purpose purpose);
        List<Manager> GetManagers();
    }
}
