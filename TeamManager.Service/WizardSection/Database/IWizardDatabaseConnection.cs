using TeamManager.Service.Models;

namespace TeamManager.Service.WizardSection.Database
{
    public interface IWizardDatabaseConnection
    {
        public void SavePurpose(Purpose purpose);
        List<Manager> GetManagers();
    }
}
