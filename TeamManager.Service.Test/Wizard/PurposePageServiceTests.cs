using Moq;
using TeamManager.Service.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;

using Xunit;

namespace TeamManager.Service.Test.Wizard
{
    public class PurposePageServiceTests
    {
        [Fact]
        public void SavePurposeOfVisit_EmptyDB_SavesPurpose()
        {
            // Arrange
            var connection = new Mock<IWizardDatabaseConnection>();

            string userName = "validUserName";
            string purposeText = "A valid purpose";

            Purpose purpose = new Purpose(userName, purposeText);

            var page = new PurposePageService(connection.Object);

            // Act
            page.SavePurposeOfVisit(purpose);

            // Assert
            connection.Verify(x => x.SavePurpose(purpose));
        }
    }
}
