using Moq;
using System;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.DatabaseControllers;
using Xunit;

namespace TeamManager.Service.UnitTest.Wizard
{
    public class PurposePageServiceTests
    {
        readonly Mock<WizardDatabaseController> databaseController;
        readonly PurposePageService page;
        public PurposePageServiceTests()
        {
            databaseController = new Mock<WizardDatabaseController>("connectionString");
            page = new PurposePageService(databaseController.Object);
        }

        [Fact]
        public void SavePurposeOfVisit_EmptyDBValidPurpose_SavesPurpose()
        {
            // Arrange
            string userName = "userName";
            string purposeText = "A valid purpose";
            Purpose purpose = new Purpose(userName, purposeText);
            databaseController.Setup(d => d.SavePurpose(purpose));

            // Act
            page.SavePurposeOfVisit(purpose);

            // Assert
            databaseController.Verify(x => x.SavePurpose(purpose));
        }

        [Fact]
        public void SavePurposeOfVisit_EmptyDBInvalidPurpose_ThrowsException()
        {
            // Arrange
            string userName = "userName";
            string invalidPurposeText = "";

            Purpose purpose = new Purpose(userName, invalidPurposeText);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => page.SavePurposeOfVisit(purpose));
        }
    }
}
