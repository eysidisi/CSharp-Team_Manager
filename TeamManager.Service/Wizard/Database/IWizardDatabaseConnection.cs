using TeamManager.Service.Models;

namespace TeamManager.Service.Wizard.Database
{
    public interface IWizardDatabaseConnection
    {
        public void SavePurpose(Purpose purpose);
        List<Manager> GetManagers();
    }
}
