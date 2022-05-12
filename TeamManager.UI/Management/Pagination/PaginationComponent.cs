using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamManager.UI.Management.Pagination
{
    public partial class PaginationComponent : UserControl
    {
        public Action<int> OnCurrentPageNumChanged;
        public int CurrentPageNumber { get;private set; }

        int maxPageNum;
        public PaginationComponent()
        {
            InitializeComponent();
        }

        public void SetMaxPageNum(int maxPageNum)
        {
            this.maxPageNum = maxPageNum;
            labelMaxNumOfPage.Text = maxPageNum.ToString();
            CurrentPageNumber = 1;
            textBoxCurrentPageNumber.Text = CurrentPageNumber.ToString();
        }

        private void textBoxPageNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string enteredPageNum = textBoxCurrentPageNumber.Text;
                ProcessEnteredPageNumber(enteredPageNum);
            }
        }

        private void ProcessEnteredPageNumber(string enteredPageNum)
        {

            if (IsPageNumberValid(enteredPageNum))
            {
                int pageNum = Convert.ToInt32(enteredPageNum);
                CurrentPageNumber = pageNum;
                textBoxCurrentPageNumber.Text = CurrentPageNumber.ToString();
                OnCurrentPageNumChanged?.Invoke(pageNum);
            }

            else
            {
                MessageBox.Show("Enter a valid value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCurrentPageNumber.Text = CurrentPageNumber.ToString();
            }
        }

        private bool IsPageNumberValid(string enteredPageNumString)
        {
            if (int.TryParse(enteredPageNumString, out int pageNumber))
            {
                if (pageNumber >= 1 && pageNumber <= maxPageNum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (CurrentPageNumber + 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (CurrentPageNumber - 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }
    }
}
