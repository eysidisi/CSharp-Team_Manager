namespace TeamManager.UI.Management.UserControls.UserPages
{
    partial class UserDetailsPage
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
            this.labelUserName = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelDataViewPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUserName.Location = new System.Drawing.Point(259, 10);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(103, 28);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "UserName";
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBack.Location = new System.Drawing.Point(232, 647);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 50);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panelDataViewPage
            // 
            this.panelDataViewPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDataViewPage.Location = new System.Drawing.Point(5, 40);
            this.panelDataViewPage.Name = "panelDataViewPage";
            this.panelDataViewPage.Size = new System.Drawing.Size(610, 600);
            this.panelDataViewPage.TabIndex = 3;
            // 
            // UserDetailsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDataViewPage);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelUserName);
            this.Name = "UserDetailsPage";
            this.Size = new System.Drawing.Size(620, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelUserName;
        private Button buttonBack;
        private Panel panelDataViewPage;
    }
}
