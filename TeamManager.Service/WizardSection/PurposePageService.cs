using TeamManager.Service.Models;
using TeamManager.Service.WizardSection.Database;
namespace TeamManager.Service.WizardSection
{
    public class PurposePageService
    {
        IWizardDatabaseConnection connection;

        public PurposePageService(IWizardDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public bool CheckIfPurposeIsValid(string purpose)
        {
            // TODO: Can add more validation rules. Length?
            if (string.IsNullOrEmpty(purpose))
                return false;

            return true;
        }

        public void SavePurposeOfVisit(Purpose purpose)
        {
            connection.SavePurpose(purpose);
        }
    }
}
