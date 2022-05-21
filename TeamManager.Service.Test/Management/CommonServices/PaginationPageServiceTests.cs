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
        const int NumberOfItems = 100;
        const int NumberOfItemsPerPage = 10;
        DataViewPageService<object> service;

        public DataViewPageServiceTests()
        {
            
        }

        [Fact]
        public void TryToGetItemsInPage_ItemsPresentValidInput_ReturnsItems()
        {
            // Arrange
            CreateServiceWithItems();

            // Act
            var items = service.TryToGetItemsInPage(5);

            // Assert
            Assert.Equal(NumberOfItemsPerPage, items.Count);
        }

        [Fact]
        public void TryToGetItemsInPage_ItemsPresentInvalidInput_ThrowsException()
        {
            // Arrange
            CreateServiceWithItems();

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.TryToGetItemsInPage(20));
        }

        [Fact]
        public void TryToGetItemsInPage_NoItemsPresent_ThrowsException()
        {
            // Arrange
            CreateEmptyService();

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => service.TryToGetItemsInPage(0));
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsValid_SetsCurrentPageNumber()
        {
            // Arrange
            CreateServiceWithItems();
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
            CreateServiceWithItems();
            string targetInvalidNumber = "a";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => service.SetCurrentPageNumber(targetInvalidNumber));
        }

        [Fact]
        public void SetCurrentPageNumber_InputIsNotInRange_ThrowsException()
        {
            // Arrange
            CreateServiceWithItems();
            string targetInvalidNumber = "11";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => service.SetCurrentPageNumber(targetInvalidNumber));
        }

        private void CreateServiceWithItems()
        {
            List<object> itemsToDisplay = new List<object>();

            for (int i = 0; i < NumberOfItems; i++)
            {
                itemsToDisplay.Add(new object());
            }

            service = new DataViewPageService<object>(itemsToDisplay, NumberOfItemsPerPage);
        }

        private void CreateEmptyService()
        {
            service = new DataViewPageService<object>(new List<object>(), NumberOfItemsPerPage);
        }
    }
}
