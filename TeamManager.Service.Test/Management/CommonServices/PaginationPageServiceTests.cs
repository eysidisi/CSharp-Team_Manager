using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.CommonServices;
using Xunit;

namespace TeamManager.Service.Test.Management.CommonServices
{
    public class DataViewPageServiceTests
    {
        const int NumberOfItems = 50;
        const int NumberOfItemsPerPage = 10;
        DataViewPageService<object> service;

        public DataViewPageServiceTests()
        {
            List<object> itemsToDisplay = new List<object>();

            for (int i = 0; i < NumberOfItems; i++)
            {
                itemsToDisplay.Add(itemsToDisplay);
            }

            service = new DataViewPageService<object>(itemsToDisplay,NumberOfItemsPerPage);
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsValid_SetsCurrentPageNumber()
        {
            // Arrange
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
            string targetInvalidNumber = "a";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => service.SetCurrentPageNumber(targetInvalidNumber));
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsNotInRange_ThrowsException()
        {
            // Arrange
            string targetInvalidNumber = "11";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => service.SetCurrentPageNumber(targetInvalidNumber));
        }
    }
}
