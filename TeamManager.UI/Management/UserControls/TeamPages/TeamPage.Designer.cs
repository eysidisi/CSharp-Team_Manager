namespace TeamManager.UI.Management.UserControls
{
    partial class TeamPage
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
            this.buttonDeleteTeam = new System.Windows.Forms.Button();
            this.buttonAddNewTeam = new System.Windows.Forms.Button();
            this.buttonTeamDetails = new System.Windows.Forms.Button();
            this.buttonEditTeam = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTeamsDataGridView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonDeleteTeam
            // 
            this.buttonDeleteTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDeleteTeam.Location = new System.Drawing.Point(310, 647);
            this.buttonDeleteTeam.Name = "buttonDeleteTeam";
            this.buttonDeleteTeam.Size = new System.Drawing.Size(150, 50);
            this.buttonDeleteTeam.TabIndex = 2;
            this.buttonDeleteTeam.Text = "Delete Selected Team";
            this.buttonDeleteTeam.UseVisualStyleBackColor = true;
            this.buttonDeleteTeam.Click += new System.EventHandler(this.buttonDeleteTeam_Click);
            // 
            // buttonAddNewTeam
            // 
            this.buttonAddNewTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddNewTeam.Location = new System.Drawing.Point(156, 647);
            this.buttonAddNewTeam.Name = "buttonAddNewTeam";
            this.buttonAddNewTeam.Size = new System.Drawing.Size(150, 50);
            this.buttonAddNewTeam.TabIndex = 3;
            this.buttonAddNewTeam.Text = "Add Team";
            this.buttonAddNewTeam.UseVisualStyleBackColor = true;
            this.buttonAddNewTeam.Click += new System.EventHandler(this.buttonAddNewTeam_Click);
            // 
            // buttonTeamDetails
            // 
            this.buttonTeamDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTeamDetails.Location = new System.Drawing.Point(3, 647);
            this.buttonTeamDetails.Name = "buttonTeamDetails";
            this.buttonTeamDetails.Size = new System.Drawing.Size(150, 50);
            this.buttonTeamDetails.TabIndex = 4;
            this.buttonTeamDetails.Text = "Team Details";
            this.buttonTeamDetails.UseVisualStyleBackColor = true;
            this.buttonTeamDetails.Click += new System.EventHandler(this.buttonTeamDetails_Click);
            // 
            // buttonEditTeam
            // 
            this.buttonEditTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonEditTeam.Location = new System.Drawing.Point(464, 647);
            this.buttonEditTeam.Name = "buttonEditTeam";
            this.buttonEditTeam.Size = new System.Drawing.Size(150, 50);
            this.buttonEditTeam.TabIndex = 5;
            this.buttonEditTeam.Text = "Edit Team";
            this.buttonEditTeam.UseVisualStyleBackColor = true;
            this.buttonEditTeam.Click += new System.EventHandler(this.buttonEditTeam_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(278, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "Teams";
            // 
            // panelTeamsDataGridView
            // 
            this.panelTeamsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTeamsDataGridView.Location = new System.Drawing.Point(5, 40);
            this.panelTeamsDataGridView.Name = "panelTeamsDataGridView";
            this.panelTeamsDataGridView.Size = new System.Drawing.Size(610, 600);
            this.panelTeamsDataGridView.TabIndex = 7;
            // 
            // TeamPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTeamsDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEditTeam);
            this.Controls.Add(this.buttonTeamDetails);
            this.Controls.Add(this.buttonAddNewTeam);
            this.Controls.Add(this.buttonDeleteTeam);
            this.Name = "TeamPage";
            this.Size = new System.Drawing.Size(620, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonDeleteTeam;
        private Button buttonAddNewTeam;
        private Button buttonTeamDetails;
        private Button buttonEditTeam;
        private Label label1;
        private Panel panelTeamsDataGridView;
    }
}
