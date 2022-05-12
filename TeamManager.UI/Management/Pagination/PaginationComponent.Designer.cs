namespace TeamManager.UI.Management.Pagination
{
    partial class PaginationComponent
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
            this.textBoxCurrentPageNumber = new System.Windows.Forms.TextBox();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMaxNumOfPage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxCurrentPageNumber
            // 
            this.textBoxCurrentPageNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCurrentPageNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxCurrentPageNumber.Location = new System.Drawing.Point(166, 40);
            this.textBoxCurrentPageNumber.Name = "textBoxCurrentPageNumber";
            this.textBoxCurrentPageNumber.Size = new System.Drawing.Size(34, 29);
            this.textBoxCurrentPageNumber.TabIndex = 0;
            this.textBoxCurrentPageNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPageNumber_KeyDown);
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNextPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonNextPage.Location = new System.Drawing.Point(219, 39);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(120, 30);
            this.buttonNextPage.TabIndex = 1;
            this.buttonNextPage.Text = "Next Page";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPreviousPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonPreviousPage.Location = new System.Drawing.Point(36, 39);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(120, 30);
            this.buttonPreviousPage.TabIndex = 2;
            this.buttonPreviousPage.Text = "Previous Page";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "1";
            // 
            // labelMaxNumOfPage
            // 
            this.labelMaxNumOfPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMaxNumOfPage.AutoSize = true;
            this.labelMaxNumOfPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMaxNumOfPage.Location = new System.Drawing.Point(345, 44);
            this.labelMaxNumOfPage.Name = "labelMaxNumOfPage";
            this.labelMaxNumOfPage.Size = new System.Drawing.Size(19, 21);
            this.labelMaxNumOfPage.TabIndex = 4;
            this.labelMaxNumOfPage.Text = "1";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(113, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Current Page Index";
            // 
            // PaginationComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelMaxNumOfPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPreviousPage);
            this.Controls.Add(this.buttonNextPage);
            this.Controls.Add(this.textBoxCurrentPageNumber);
            this.Name = "PaginationComponent";
            this.Size = new System.Drawing.Size(378, 96);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxCurrentPageNumber;
        private Button buttonNextPage;
        private Button buttonPreviousPage;
        private Label label1;
        private Label labelMaxNumOfPage;
        private Label label2;
    }
}
