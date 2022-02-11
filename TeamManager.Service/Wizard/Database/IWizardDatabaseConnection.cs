using TeamManager.Service.Models;

namespace TeamManager.Service.Wizard.Database
{
    public interface IWizardDatabaseConnection
    {
        List<Purpose> GetPurposes(string userName);
        public void SavePurpose(Purpose purpose);
        Manager GetManager(string userName);
    }
}
