using Moq;
using System;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;

using Xunit;

namespace TeamManager.Service.UnitTest.Wizard
{
    public class PurposePageServiceTests
    {
        readonly Mock<IWizardDatabaseConnection> connection;
        readonly PurposePageService page;
        public PurposePageServiceTests()
        {
            connection = new Mock<IWizardDatabaseConnection>();
            page = new PurposePageService(connection.Object);

        }

        [Fact]
        public void SavePurposeOfVisit_EmptyDBValidPurpose_SavesPurpose()
        {
            // Arrange
            string userName = "userName";
            string purposeText = "A valid purpose";
            Purpose purpose = new Purpose(userName, purposeText);

            // Act
            page.SavePurposeOfVisit(purpose);

            // Assert
            connection.Verify(x => x.SavePurpose(purpose));
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
