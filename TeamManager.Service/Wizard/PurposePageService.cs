using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.DatabaseControllers;

namespace TeamManager.Service.Wizard
{
    public class PurposePageService
    {
        readonly WizardDatabaseController databaseManager;

        public PurposePageService(WizardDatabaseController databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public void SavePurposeOfVisit(Purpose purpose)
        {
            if (CheckIfPurposeIsValid(purpose) == false)
            {
                throw new ArgumentException("Purpose is not valid!");
            }
            databaseManager.SavePurpose(purpose);
        }

        private bool CheckIfPurposeIsValid(Purpose purpose)
        {
            // TODO: Can add more validation rules. Length?
            if (string.IsNullOrEmpty(purpose.PurposeText))
            {
                return false;
            }

            return true;
        }
    }
}
