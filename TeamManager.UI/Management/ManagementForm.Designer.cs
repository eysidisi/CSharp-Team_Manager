namespace TeamManager.UI.Management
{
    partial class ManagementForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelUserDetails = new System.Windows.Forms.Panel();
            this.panelTeamPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelUserDetails
            // 
            this.panelUserDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserDetails.Location = new System.Drawing.Point(636, 60);
            this.panelUserDetails.Name = "panelUserDetails";
            this.panelUserDetails.Size = new System.Drawing.Size(620, 600);
            this.panelUserDetails.TabIndex = 0;
            // 
            // panelTeamPage
            // 
            this.panelTeamPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTeamPage.Location = new System.Drawing.Point(10, 60);
            this.panelTeamPage.Name = "panelTeamPage";
            this.panelTeamPage.Size = new System.Drawing.Size(620, 600);
            this.panelTeamPage.TabIndex = 1;
            // 
            // ManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 741);
            this.Controls.Add(this.panelTeamPage);
            this.Controls.Add(this.panelUserDetails);
            this.Name = "ManagementForm";
            this.Text = "Management";
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelUserDetails;
        private Panel panelTeamPage;
    }
}