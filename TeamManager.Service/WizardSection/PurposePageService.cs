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

        private bool CheckIfPurposeIsValid(Purpose purpose)
        {
            // TODO: Can add more validation rules. Length?
            if (string.IsNullOrEmpty(purpose.PurposeText))
                return false;

            return true;
        }

        public void SavePurposeOfVisit(Purpose purpose)
        {
            if (CheckIfPurposeIsValid(purpose) == false)
            {
                throw new ArgumentException("Purpuse is not valid!");
            }
            connection.SavePurpose(purpose);
        }
    }
}
