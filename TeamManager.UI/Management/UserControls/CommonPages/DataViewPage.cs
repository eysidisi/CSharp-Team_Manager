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
using TeamManager.Service.Management.CommonServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class DataViewPage<DataTypeToView> : UserControl
    {
        DataViewPageService<DataTypeToView> dataViewPageService;
        public DataViewPage(Panel panelToFit)
        {
            InitializeComponent();
            ResizeAndDisplayPage(panelToFit);
        }

        private void ResizeAndDisplayPage(Panel panelToFit)
        {
            panelToFit.Controls.Add(this);
            Size = panelToFit.Size;
            panelToFit.Controls.Add(this);
        }

        public void SetUpPage( List<DataTypeToView> itemsToDisplay)
        {
            dataViewPageService = new DataViewPageService<DataTypeToView>(itemsToDisplay);
            labelMaxNumOfPage.Text = dataViewPageService.NumOfMaximumPages.ToString();
            textBoxCurrentPageNumber.Text = dataViewPageService.CurrentPageNumber.ToString();
            ViewItemsInPage(1);
        }

        private void ViewItemsInPage(int pageNum)
        {
            var itemsToDisplay = dataViewPageService.TryToGetItemsInPage(pageNum);
            var teamsDataTable = HelperFunctions.ConvertListToDatatable(itemsToDisplay);
            dataGridView.DataSource = teamsDataTable;
            ResizeColumns(dataGridView);
        }
        
        private void ResizeColumns(DataGridView dataGrid)
        {
            int width = dataGrid.Width;
            int minColWidth = (int)Math.Ceiling(width / (double)dataGrid.Columns.Count);
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].MinimumWidth = minColWidth;
            }
        }

        public void DeleteSelectedRow()
        {
            (dataGridView.SelectedRows[0].DataBoundItem as DataRowView).Delete();
        }

        public DataRowView GetSelectedRow()
        {
            if (dataGridView.SelectedRows.Count < 1)
            {
                throw new Exception("No item is selected! Please select an item first!");
            }
            DataRowView selectedRow = dataGridView.SelectedRows[0].DataBoundItem as DataRowView;
 
            return selectedRow;
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
                dataViewPageService.SetCurrentPageNumber(enteredPageNum);
                ViewItemsInPage(dataViewPageService.CurrentPageNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBoxCurrentPageNumber.Text = dataViewPageService.CurrentPageNumber.ToString();
            }
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (dataViewPageService.CurrentPageNumber + 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            string newPageNumber = (dataViewPageService.CurrentPageNumber - 1).ToString();
            ProcessEnteredPageNumber(newPageNumber);
        }
    }
}
