namespace TeamManager.UI.Management.UserControls
{
    partial class TeamDetailsPage
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelUserssDataGridView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelTeamName
            // 
            this.labelTeamName.AutoSize = true;
            this.labelTeamName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTeamName.Location = new System.Drawing.Point(277, 10);
            this.labelTeamName.Name = "labelTeamName";
            this.labelTeamName.Size = new System.Drawing.Size(109, 28);
            this.labelTeamName.TabIndex = 1;
            this.labelTeamName.Text = "TeamName";
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBack.Location = new System.Drawing.Point(236, 647);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 50);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panelUserssDataGridView
            // 
            this.panelUserssDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserssDataGridView.Location = new System.Drawing.Point(5, 40);
            this.panelUserssDataGridView.Name = "panelUserssDataGridView";
            this.panelUserssDataGridView.Size = new System.Drawing.Size(610, 600);
            this.panelUserssDataGridView.TabIndex = 8;
            // 
            // TeamDetailsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelUserssDataGridView);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelTeamName);
            this.Name = "TeamDetailsPage";
            this.Size = new System.Drawing.Size(620, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelTeamName;
        private Button buttonBack;
        private Panel panelUserssDataGridView;
    }
}
