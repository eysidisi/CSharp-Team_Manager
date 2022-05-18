namespace TeamManager.UI.Management.UserControls
{
    partial class UserPage
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
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonUserDetails = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDataViewPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDeleteUser.Location = new System.Drawing.Point(244, 647);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(150, 50);
            this.buttonDeleteUser.TabIndex = 1;
            this.buttonDeleteUser.Text = "Delete Selected User";
            this.buttonDeleteUser.UseVisualStyleBackColor = true;
            this.buttonDeleteUser.Click += new System.EventHandler(this.buttonDeleteUser_Click);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddUser.Location = new System.Drawing.Point(88, 647);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(150, 50);
            this.buttonAddUser.TabIndex = 2;
            this.buttonAddUser.Text = "Add User";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // buttonUserDetails
            // 
            this.buttonUserDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonUserDetails.Location = new System.Drawing.Point(400, 647);
            this.buttonUserDetails.Name = "buttonUserDetails";
            this.buttonUserDetails.Size = new System.Drawing.Size(150, 50);
            this.buttonUserDetails.TabIndex = 3;
            this.buttonUserDetails.Text = "User Details";
            this.buttonUserDetails.UseVisualStyleBackColor = true;
            this.buttonUserDetails.Click += new System.EventHandler(this.buttonUserDetails_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(281, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Users";
            // 
            // panelDataViewPage
            // 
            this.panelDataViewPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDataViewPage.Location = new System.Drawing.Point(5, 40);
            this.panelDataViewPage.Name = "panelDataViewPage";
            this.panelDataViewPage.Size = new System.Drawing.Size(610, 600);
            this.panelDataViewPage.TabIndex = 5;
            // 
            // UserPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDataViewPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUserDetails);
            this.Controls.Add(this.buttonDeleteUser);
            this.Controls.Add(this.buttonAddUser);
            this.Name = "UserPage";
            this.Size = new System.Drawing.Size(620, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonDeleteUser;
        private Button buttonAddUser;
        private Button buttonUserDetails;
        private Label label1;
        private Panel panelDataViewPage;
    }
}
