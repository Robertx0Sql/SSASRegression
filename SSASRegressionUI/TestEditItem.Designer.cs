namespace SSASRegressionUI
{
    partial class TestEditItem
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnEditMDX = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMDX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(70, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(397, 20);
            this.txtID.TabIndex = 13;
            // 
            // btnEditMDX
            // 
            this.btnEditMDX.Location = new System.Drawing.Point(7, 90);
            this.btnEditMDX.Name = "btnEditMDX";
            this.btnEditMDX.Size = new System.Drawing.Size(57, 23);
            this.btnEditMDX.TabIndex = 12;
            this.btnEditMDX.Text = "Edit Mdx";
            this.btnEditMDX.UseVisualStyleBackColor = true;
            this.btnEditMDX.Click += new System.EventHandler(this.btnEditMDX_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mdx Test";
            // 
            // txtMDX
            // 
            this.txtMDX.AcceptsReturn = true;
            this.txtMDX.AcceptsTab = true;
            this.txtMDX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMDX.Location = new System.Drawing.Point(70, 49);
            this.txtMDX.Multiline = true;
            this.txtMDX.Name = "txtMDX";
            this.txtMDX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMDX.Size = new System.Drawing.Size(397, 131);
            this.txtMDX.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(70, 23);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(397, 20);
            this.txtDescription.TabIndex = 8;
            // 
            // TestEditItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnEditMDX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMDX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescription);
            this.Name = "TestEditItem";
            this.Size = new System.Drawing.Size(476, 187);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnEditMDX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMDX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;

    }
}
