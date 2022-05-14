namespace TeamManager.UI.Management.UserControls
{
    partial class EditTeamPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTeamUsers = new System.Windows.Forms.DataGridView();
            this.labelTeamName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsers = new System.Windows.Forms.Label();
            this.buttonAddSelectedUser = new System.Windows.Forms.Button();
            this.buttonRemoveSelectedUser = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewAllUsers = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.paginationPageAllUsers = new TeamManager.UI.Management.UserControls.PaginationPage();
            this.paginationPageTeamUsers = new TeamManager.UI.Management.UserControls.PaginationPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeamUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTeamUsers
            // 
            this.dataGridViewTeamUsers.AllowUserToAddRows = false;
            this.dataGridViewTeamUsers.AllowUserToDeleteRows = false;
            this.dataGridViewTeamUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewTeamUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTeamUsers.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTeamUsers.Location = new System.Drawing.Point(3, 50);
            this.dataGridViewTeamUsers.MultiSelect = false;
            this.dataGridViewTeamUsers.Name = "dataGridViewTeamUsers";
            this.dataGridViewTeamUsers.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTeamUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTeamUsers.RowHeadersVisible = false;
            this.dataGridViewTeamUsers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewTeamUsers.RowTemplate.Height = 25;
            this.dataGridViewTeamUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTeamUsers.Size = new System.Drawing.Size(300, 350);
            this.dataGridViewTeamUsers.TabIndex = 0;
            // 
            // labelTeamName
            // 
            this.labelTeamName.AutoSize = true;
            this.labelTeamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTeamName.Location = new System.Drawing.Point(269, 10);
            this.labelTeamName.Name = "labelTeamName";
            this.labelTeamName.Size = new System.Drawing.Size(74, 25);
            this.labelTeamName.TabIndex = 2;
            this.labelTeamName.Text = "Team1";
            this.labelTeamName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(78, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Team Members";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUsers.Location = new System.Drawing.Point(429, 22);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(63, 25);
            this.labelUsers.TabIndex = 4;
            this.labelUsers.Text = "Users";
            this.labelUsers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAddSelectedUser
            // 
            this.buttonAddSelectedUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddSelectedUser.Location = new System.Drawing.Point(382, 491);
            this.buttonAddSelectedUser.Name = "buttonAddSelectedUser";
            this.buttonAddSelectedUser.Size = new System.Drawing.Size(165, 50);
            this.buttonAddSelectedUser.TabIndex = 5;
            this.buttonAddSelectedUser.Text = "Add Selected User To The Team";
            this.buttonAddSelectedUser.UseVisualStyleBackColor = true;
            this.buttonAddSelectedUser.Click += new System.EventHandler(this.buttonAddSelectedUser_Click);
            // 
            // buttonRemoveSelectedUser
            // 
            this.buttonRemoveSelectedUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRemoveSelectedUser.Location = new System.Drawing.Point(75, 491);
            this.buttonRemoveSelectedUser.Name = "buttonRemoveSelectedUser";
            this.buttonRemoveSelectedUser.Size = new System.Drawing.Size(165, 50);
            this.buttonRemoveSelectedUser.TabIndex = 6;
            this.buttonRemoveSelectedUser.Text = "Remove Selected User From The Team";
            this.buttonRemoveSelectedUser.UseVisualStyleBackColor = true;
            this.buttonRemoveSelectedUser.Click += new System.EventHandler(this.buttonRemoveSelectedUser_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(240, 547);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 50);
            this.button2.TabIndex = 7;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // dataGridViewAllUsers
            // 
            this.dataGridViewAllUsers.AllowUserToAddRows = false;
            this.dataGridViewAllUsers.AllowUserToDeleteRows = false;
            this.dataGridViewAllUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAllUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllUsers.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewAllUsers.Location = new System.Drawing.Point(310, 50);
            this.dataGridViewAllUsers.MultiSelect = false;
            this.dataGridViewAllUsers.Name = "dataGridViewAllUsers";
            this.dataGridViewAllUsers.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAllUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewAllUsers.RowHeadersVisible = false;
            this.dataGridViewAllUsers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewAllUsers.RowTemplate.Height = 25;
            this.dataGridViewAllUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAllUsers.Size = new System.Drawing.Size(300, 350);
            this.dataGridViewAllUsers.TabIndex = 8;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRefresh.Location = new System.Drawing.Point(269, 491);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(78, 50);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "Refresh Users";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // paginationPageAllUsers
            // 
            this.paginationPageAllUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paginationPageAllUsers.Location = new System.Drawing.Point(310, 405);
            this.paginationPageAllUsers.Name = "paginationPageAllUsers";
            this.paginationPageAllUsers.Size = new System.Drawing.Size(300, 80);
            this.paginationPageAllUsers.TabIndex = 10;
            // 
            // paginationPageTeamUsers
            // 
            this.paginationPageTeamUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paginationPageTeamUsers.Location = new System.Drawing.Point(4, 405);
            this.paginationPageTeamUsers.Name = "paginationPageTeamUsers";
            this.paginationPageTeamUsers.Size = new System.Drawing.Size(300, 80);
            this.paginationPageTeamUsers.TabIndex = 11;
            // 
            // EditTeamPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paginationPageTeamUsers);
            this.Controls.Add(this.paginationPageAllUsers);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.dataGridViewAllUsers);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonRemoveSelectedUser);
            this.Controls.Add(this.buttonAddSelectedUser);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTeamName);
            this.Controls.Add(this.dataGridViewTeamUsers);
            this.Name = "EditTeamPage";
            this.Size = new System.Drawing.Size(620, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeamUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridViewTeamUsers;
        private Label labelTeamName;
        private Label label1;
        private Label labelUsers;
        private Button buttonAddSelectedUser;
        private Button buttonRemoveSelectedUser;
        private Button button2;
        private DataGridView dataGridViewAllUsers;
        private Button buttonRefresh;
        private PaginationPage paginationPageAllUsers;
        private PaginationPage paginationPageTeamUsers;
    }
}
