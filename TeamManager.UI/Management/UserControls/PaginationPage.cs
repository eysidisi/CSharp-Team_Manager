using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Management;

namespace TeamManager.UI.Management.UserControls
{
    public partial class PaginationPage : UserControl
    {
        public Action<int> OnCurrentPageNumChanged;
        PaginationPageService paginationService;

        public PaginationPage()
        {
            InitializeComponent();
        }

        public void SetMaxPageNum(int maxPageNum)
        {
            paginationService = new PaginationPageService(maxPageNum);
            labelMaxNumOfPage.Text = maxPageNum.ToString();
            textBoxCurrentPageNumber.Text = paginationService.CurrentPageNumber.ToString();
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
            try
            {
                paginationService.SetCurrentPageNumberIntoInteger(enteredPageNum);
                OnCurrentPageNumChanged?.Invoke(paginationService.CurrentPageNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBoxCurrentPageNumber.Text = paginationService.CurrentPageNumber.ToString();
            }
        }


        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (paginationService.CurrentPageNumber + 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (paginationService.CurrentPageNumber - 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }
    }
}
