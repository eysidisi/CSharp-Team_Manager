using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Database;
using TeamManager.UI.UserControls;

namespace TeamManager.UI
{
    public partial class ManagerForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\TestDB.db; Version = 3";

        public ManagerForm()
        {
            InitializeComponent();
            IDatabaseConnection connection = new SQLiteDataAccess(connectionString);
            UserPageUserControl userPageUser = new UserPageUserControl(connection);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
