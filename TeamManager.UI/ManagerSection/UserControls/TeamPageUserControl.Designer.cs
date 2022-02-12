namespace TeamManager.UI.ManagerSection.UserControls
{
    partial class TeamPageUserControl
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
            this.dataGridViewTeams = new System.Windows.Forms.DataGridView();
            this.buttonDeleteTeam = new System.Windows.Forms.Button();
            this.buttonAddNewTeam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeams)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTeams
            // 
            this.dataGridViewTeams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTeams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTeams.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTeams.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTeams.Name = "dataGridViewTeams";
            this.dataGridViewTeams.ReadOnly = true;
            this.dataGridViewTeams.RowTemplate.Height = 25;
            this.dataGridViewTeams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTeams.Size = new System.Drawing.Size(495, 350);
            this.dataGridViewTeams.TabIndex = 1;
            // 
            // buttonDeleteTeam
            // 
            this.buttonDeleteTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDeleteTeam.Location = new System.Drawing.Point(160, 451);
            this.buttonDeleteTeam.Name = "buttonDeleteTeam";
            this.buttonDeleteTeam.Size = new System.Drawing.Size(128, 50);
            this.buttonDeleteTeam.TabIndex = 2;
            this.buttonDeleteTeam.Text = "Delete Selected Team";
            this.buttonDeleteTeam.UseVisualStyleBackColor = true;
            this.buttonDeleteTeam.Click += new System.EventHandler(this.buttonDeleteTeam_Click);
            // 
            // buttonAddNewTeam
            // 
            this.buttonAddNewTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddNewTeam.Location = new System.Drawing.Point(160, 395);
            this.buttonAddNewTeam.Name = "buttonAddNewTeam";
            this.buttonAddNewTeam.Size = new System.Drawing.Size(128, 50);
            this.buttonAddNewTeam.TabIndex = 3;
            this.buttonAddNewTeam.Text = "Add Team";
            this.buttonAddNewTeam.UseVisualStyleBackColor = true;
            this.buttonAddNewTeam.Click += new System.EventHandler(this.buttonAddNewTeam_Click);
            // 
            // TeamPageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAddNewTeam);
            this.Controls.Add(this.buttonDeleteTeam);
            this.Controls.Add(this.dataGridViewTeams);
            this.Name = "TeamPageUserControl";
            this.Size = new System.Drawing.Size(500, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTeams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridViewTeams;
        private Button buttonDeleteTeam;
        private Button buttonAddNewTeam;
    }
}
