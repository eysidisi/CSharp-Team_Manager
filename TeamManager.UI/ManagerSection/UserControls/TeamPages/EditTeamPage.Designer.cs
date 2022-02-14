namespace TeamManager.UI.ManagerSection.UserControls
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTeamUsers = new System.Windows.Forms.DataGridView();
            this.labelTeamName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsers = new System.Windows.Forms.Label();
            this.buttonAddSelectedUser = new System.Windows.Forms.Button();
            this.buttonRemoveSelectedUser = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeamUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTeamUsers
            // 
            this.dataGridViewTeamUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewTeamUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTeamUsers.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTeamUsers.Location = new System.Drawing.Point(3, 60);
            this.dataGridViewTeamUsers.Name = "dataGridViewTeamUsers";
            this.dataGridViewTeamUsers.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTeamUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            this.label1.Location = new System.Drawing.Point(78, 32);
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
            this.labelUsers.Location = new System.Drawing.Point(429, 32);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(63, 25);
            this.labelUsers.TabIndex = 4;
            this.labelUsers.Text = "Users";
            this.labelUsers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAddSelectedUser
            // 
            this.buttonAddSelectedUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddSelectedUser.Location = new System.Drawing.Point(160, 436);
            this.buttonAddSelectedUser.Name = "buttonAddSelectedUser";
            this.buttonAddSelectedUser.Size = new System.Drawing.Size(300, 50);
            this.buttonAddSelectedUser.TabIndex = 5;
            this.buttonAddSelectedUser.Text = "Add Selected User To The Team";
            this.buttonAddSelectedUser.UseVisualStyleBackColor = true;
            this.buttonAddSelectedUser.Click += new System.EventHandler(this.buttonAddSelectedUser_Click);
            // 
            // buttonRemoveSelectedUser
            // 
            this.buttonRemoveSelectedUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRemoveSelectedUser.Location = new System.Drawing.Point(160, 490);
            this.buttonRemoveSelectedUser.Name = "buttonRemoveSelectedUser";
            this.buttonRemoveSelectedUser.Size = new System.Drawing.Size(300, 50);
            this.buttonRemoveSelectedUser.TabIndex = 6;
            this.buttonRemoveSelectedUser.Text = "Remove Selected User From The Team";
            this.buttonRemoveSelectedUser.UseVisualStyleBackColor = true;
            this.buttonRemoveSelectedUser.Click += new System.EventHandler(this.buttonRemoveSelectedUser_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(160, 544);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(300, 50);
            this.button2.TabIndex = 7;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUsers.Location = new System.Drawing.Point(310, 60);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewUsers.RowHeadersVisible = false;
            this.dataGridViewUsers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewUsers.RowTemplate.Height = 25;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(300, 350);
            this.dataGridViewUsers.TabIndex = 8;
            // 
            // EditTeamPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewUsers);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
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
        private DataGridView dataGridViewUsers;
    }
}
