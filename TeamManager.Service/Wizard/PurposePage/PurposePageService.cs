using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard.Database;

namespace TeamManager.Service.Wizard.PurposePage
{
    public class PurposePageService
    {
        IDatabaseConnection connection;

        public PurposePageService(IDatabaseConnection connection)
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

        public void SavePurposeOfVisit(string purposeText, User user)
        {
            Purpose purpose = new Purpose(user.UserName, purposeText);
            connection.SavePurpose(purpose);
        }
    }
}
