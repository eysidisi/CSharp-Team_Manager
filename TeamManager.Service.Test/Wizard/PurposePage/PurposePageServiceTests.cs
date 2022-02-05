using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.PurposePage;
using Xunit;

namespace TeamManager.Service.Test.Wizard.PurposePage
{
    public class PurposePageServiceTests
    {
        [Fact]
        public void CheckIfPurposeIsValid_ReturnsTrue()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();
            var page = new PurposePageService(connection.Object);
            string purpose = "A valid purpose";

            // Act
            var purposeValidation = page.CheckIfPurposeIsValid(purpose);

            // Assert
            Assert.True(purposeValidation);
        }

        [Fact]
        public void CheckIfPurposeIsValid_ReturnsFalse()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();
            var page = new PurposePageService(connection.Object);
            string purpose = "";

            // Act
            var purposeValidation = page.CheckIfPurposeIsValid(purpose);

            // Assert
            Assert.False(purposeValidation);
        }

        [Fact]
        public void SavePurposeOfVisitToTheDB_ReturnsFalse()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();
            var page = new PurposePageService(connection.Object);
            User user = new User();

            string purpose = "A valid purpose";

            // Act
            page.SavePurposeOfVisit(purpose,user);

            // Assert
        }
    }
}
