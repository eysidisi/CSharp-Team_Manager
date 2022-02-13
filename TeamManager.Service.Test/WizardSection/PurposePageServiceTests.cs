using Moq;
using TeamManager.Service.Models;
using TeamManager.Service.WizardSection;
using TeamManager.Service.WizardSection.Database;

using Xunit;

namespace TeamManager.Service.Test.WizardSection
{
    public class PurposePageServiceTests
    {
        [Fact]
        public void SavePurposeOfVisitToTheDB()
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
