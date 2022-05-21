﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Management.CommonServices
{
    public class DataViewPageService<T>
    {
        int numOfItemsPerPage;
        public int CurrentPageNumber { get; private set; }
        public int NumOfMaximumPages => CalculateNumberOfMaximumPages();

        List<T> items;

        public DataViewPageService(List<T> items, int numOfItemsPerPage = 15)
        {
            this.items = items;
            this.numOfItemsPerPage = numOfItemsPerPage;
            CurrentPageNumber = 1;
        }

        private int CalculateNumberOfMaximumPages()
        {
            return (int)Math.Ceiling((double)items.Count / (numOfItemsPerPage));
        }

        public List<T> TryToGetItemsInPage(int pageNum)
        {
            if (IsItemsListEmpty())
            {
                throw new InvalidOperationException("No items present!");
            }

            else if (IsPageNumberIsNotInRange(pageNum))
            {
                throw new ArgumentOutOfRangeException("Page number is not in range!");
            }

            else
            {
                return GetItemsInPage(pageNum);
            }
        }

        private bool IsItemsListEmpty()
        {
            return items.Count == 0;
        }

        private List<T> GetItemsInPage(int pageNum)
        {
            Range range = GetRangeOfItemsInPage(pageNum);
            return items.Take(range).ToList();
        }

        private Range GetRangeOfItemsInPage(int pageNum)
        {
            int startingIndexInList = ((pageNum - 1) * numOfItemsPerPage);
            int endingIndexInList = startingIndexInList + numOfItemsPerPage;
            Range range = new Range(startingIndexInList, endingIndexInList);
            return range;
        }

        public void SetCurrentPageNumber(string enteredPageNum)
        {
            if (IsPageNumberAnInteger(enteredPageNum) == false)
            {
                throw new ArgumentException("The entered page number must be an integer!");
            }

            int pageNumberInteger = int.Parse(enteredPageNum);

            if (IsPageNumberIsInRange(pageNumberInteger) == false)
            {
                throw new ArgumentException("The entered page number must be in the minimum and maximum range!");
            }

            CurrentPageNumber = pageNumberInteger;
        }

        private bool IsPageNumberAnInteger(string enteredPageNum)
        {
            return int.TryParse(enteredPageNum, out int _);
        }

        private bool IsPageNumberIsNotInRange(int pageNumberInteger)
        {
            return !IsPageNumberIsInRange(pageNumberInteger);
        }

        private bool IsPageNumberIsInRange(int pageNumberInteger)
        {
            return pageNumberInteger >= 1 && pageNumberInteger <= NumOfMaximumPages;
        }
    }
}
