using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Management.CommonServices
{
    public class PaginationPageService
    {
        private int maxPageNum;
        public int CurrentPageNumber { get; private set; }

        public PaginationPageService(int maxPageNum)
        {
            this.maxPageNum = maxPageNum;
            CurrentPageNumber = 1;
        }

        public void SetCurrentPageNumber(string enteredPageNum)
        {
            if (IsPageNumberAnInteger(enteredPageNum) == false)
            {
                throw new ArgumentException("The entered page number must be an integer!");
            }

            int pageNumberInteger = int.Parse(enteredPageNum);

            if (IsPageNumberInRange(pageNumberInteger) == false)
            {
                throw new ArgumentException("The entered page number must be in the minimum and maximum range!");
            }

            CurrentPageNumber = pageNumberInteger;
        }

        private bool IsPageNumberAnInteger(string enteredPageNum)
        {
            return int.TryParse(enteredPageNum, out int _);
        }

        private bool IsPageNumberInRange(int pageNumberInteger)
        {
            return pageNumberInteger >= 1 && pageNumberInteger <= maxPageNum;
        }
    }
}
