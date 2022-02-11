namespace TeamManager.UI.UserControls
{
    partial class PurposePageUserControl
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
            this.textBoxPurpose = new System.Windows.Forms.TextBox();
            this.buttonSavePurpose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxPurpose
            // 
            this.textBoxPurpose.Location = new System.Drawing.Point(140, 115);
            this.textBoxPurpose.Multiline = true;
            this.textBoxPurpose.Name = "textBoxPurpose";
            this.textBoxPurpose.Size = new System.Drawing.Size(221, 159);
            this.textBoxPurpose.TabIndex = 0;
            // 
            // buttonSavePurpose
            // 
            this.buttonSavePurpose.Location = new System.Drawing.Point(200, 280);
            this.buttonSavePurpose.Name = "buttonSavePurpose";
            this.buttonSavePurpose.Size = new System.Drawing.Size(88, 45);
            this.buttonSavePurpose.TabIndex = 1;
            this.buttonSavePurpose.Text = "Save Purpose";
            this.buttonSavePurpose.UseVisualStyleBackColor = true;
            this.buttonSavePurpose.Click += new System.EventHandler(this.buttonSavePurpose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // PurposePageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSavePurpose);
            this.Controls.Add(this.textBoxPurpose);
            this.Name = "PurposePageUserControl";
            this.Size = new System.Drawing.Size(557, 423);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxPurpose;
        private Button buttonSavePurpose;
        private Label label1;
    }
}
