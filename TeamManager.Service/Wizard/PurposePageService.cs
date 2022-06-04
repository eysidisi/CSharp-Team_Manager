using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard.DatabaseController;

namespace TeamManager.Service.Wizard
{
    public class PurposePageService
    {
        readonly WizardDatabaseController databaseController;

        public PurposePageService(WizardDatabaseController databaseController)
        {
            this.databaseController = databaseController;
        }

        public void SavePurposeOfVisit(Purpose purpose)
        {
            if (CheckIfPurposeIsValid(purpose) == false)
            {
                throw new ArgumentException("Purpose is not valid!");
            }
            databaseController.SavePurpose(purpose);
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
