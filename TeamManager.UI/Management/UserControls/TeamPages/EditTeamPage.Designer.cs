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
            this.labelTeamName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUsers = new System.Windows.Forms.Label();
            this.buttonAddSelectedUser = new System.Windows.Forms.Button();
            this.buttonRemoveSelectedUser = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panelAllUsersDataViewPage = new System.Windows.Forms.Panel();
            this.panelTeamUsersDataViewPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
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
            this.buttonAddSelectedUser.Location = new System.Drawing.Point(377, 556);
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
            this.buttonRemoveSelectedUser.Location = new System.Drawing.Point(63, 556);
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
            this.button2.Location = new System.Drawing.Point(243, 643);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 50);
            this.button2.TabIndex = 7;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRefresh.Location = new System.Drawing.Point(269, 587);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(78, 50);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "Refresh Users";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // panelAllUsersDataViewPage
            // 
            this.panelAllUsersDataViewPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAllUsersDataViewPage.Location = new System.Drawing.Point(309, 50);
            this.panelAllUsersDataViewPage.Name = "panelAllUsersDataViewPage";
            this.panelAllUsersDataViewPage.Size = new System.Drawing.Size(300, 500);
            this.panelAllUsersDataViewPage.TabIndex = 12;
            // 
            // panelTeamUsersDataViewPage
            // 
            this.panelTeamUsersDataViewPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTeamUsersDataViewPage.Location = new System.Drawing.Point(3, 50);
            this.panelTeamUsersDataViewPage.Name = "panelTeamUsersDataViewPage";
            this.panelTeamUsersDataViewPage.Size = new System.Drawing.Size(300, 500);
            this.panelTeamUsersDataViewPage.TabIndex = 13;
            // 
            // EditTeamPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTeamUsersDataViewPage);
            this.Controls.Add(this.panelAllUsersDataViewPage);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonRemoveSelectedUser);
            this.Controls.Add(this.buttonAddSelectedUser);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTeamName);
            this.Name = "EditTeamPage";
            this.Size = new System.Drawing.Size(620, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelTeamName;
        private Label label1;
        private Label labelUsers;
        private Button buttonAddSelectedUser;
        private Button buttonRemoveSelectedUser;
        private Button button2;
        private Button buttonRefresh;
        private Panel panelAllUsersDataViewPage;
        private Panel panelTeamUsersDataViewPage;
    }
}
