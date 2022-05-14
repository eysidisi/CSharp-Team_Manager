using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.CommonServices;
using Xunit;

namespace TeamManager.Service.Test.Management.CommonServices
{
    public class PaginationPageServiceTests
    {

        [Fact]
        public void SetCurrentPageNumber_InputIsValid_SetsCurrentPageNumber()
        {
            // Arrange
            int maxPageNum = 10;
            PaginationPageService service = new PaginationPageService(maxPageNum);
            string targetValidNumber = "5";

            // Act
            service.SetCurrentPageNumber(targetValidNumber);

            // Assert
            Assert.Equal(int.Parse(targetValidNumber), service.CurrentPageNumber);
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsNotAnInteger_ThrowsException()
        {
            // Arrange
            int maxPageNum = 10;
            PaginationPageService service = new PaginationPageService(maxPageNum);
            string targetInvalidNumber = "a";

            // Act && Assert
            Assert.Throws<ArgumentException>(()=> service.SetCurrentPageNumber(targetInvalidNumber));
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsNotInRange_ThrowsException()
        {
            // Arrange
            int maxPageNum = 10;
            PaginationPageService service = new PaginationPageService(maxPageNum);
            string targetInvalidNumber = "11";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => service.SetCurrentPageNumber(targetInvalidNumber));
        }
    }
}
