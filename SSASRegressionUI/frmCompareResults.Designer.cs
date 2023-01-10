namespace SSASRegressionUI
{
    partial class frmCompareResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompareResults));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewCellSetLeft = new SSASRegressionUI.DataGridViewCellSetAdvanced();
            this.dataGridViewCellSetRight = new SSASRegressionUI.DataGridViewCellSetAdvanced();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnCompareResultstoClipBoard = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.txtTestID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTestDescription = new System.Windows.Forms.TextBox();
            this.Test = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.testsToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.testspassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsfailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsuniqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxResults = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripDropDownButtonShowCellProp = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 572);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1078, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewCellSetLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewCellSetRight);
            this.splitContainer1.Size = new System.Drawing.Size(1078, 424);
            this.splitContainer1.SplitterDistance = 535;
            this.splitContainer1.TabIndex = 8;
            // 
            // dataGridViewCellSetLeft
            // 
            this.dataGridViewCellSetLeft.AllowUserToAddRows = true;
            this.dataGridViewCellSetLeft.AllowUserToDeleteRows = true;
            this.dataGridViewCellSetLeft.CellProperty = null;
            this.dataGridViewCellSetLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCellSetLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCellSetLeft.FileName = "";
            this.dataGridViewCellSetLeft.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCellSetLeft.Name = "dataGridViewCellSetLeft";
            this.dataGridViewCellSetLeft.result = null;
            this.dataGridViewCellSetLeft.Size = new System.Drawing.Size(535, 424);
            this.dataGridViewCellSetLeft.TabIndex = 0;
            // 
            // dataGridViewCellSetRight
            // 
            this.dataGridViewCellSetRight.AllowUserToAddRows = true;
            this.dataGridViewCellSetRight.AllowUserToDeleteRows = true;
            this.dataGridViewCellSetRight.CellProperty = null;
            this.dataGridViewCellSetRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCellSetRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCellSetRight.FileName = "";
            this.dataGridViewCellSetRight.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCellSetRight.Name = "dataGridViewCellSetRight";
            this.dataGridViewCellSetRight.result = null;
            this.dataGridViewCellSetRight.Size = new System.Drawing.Size(539, 424);
            this.dataGridViewCellSetRight.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnCompareResultstoClipBoard);
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.txtTestID);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.txtTestDescription);
            this.splitContainer2.Panel1.Controls.Add(this.Test);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1078, 547);
            this.splitContainer2.SplitterDistance = 119;
            this.splitContainer2.TabIndex = 9;
            // 
            // btnCompareResultstoClipBoard
            // 
            this.btnCompareResultstoClipBoard.Enabled = false;
            this.btnCompareResultstoClipBoard.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCompareResultstoClipBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompareResultstoClipBoard.Image = global::SSASRegressionUI.Properties.Resources.CopyHS;
            this.btnCompareResultstoClipBoard.Location = new System.Drawing.Point(24, 68);
            this.btnCompareResultstoClipBoard.Name = "btnCompareResultstoClipBoard";
            this.btnCompareResultstoClipBoard.Size = new System.Drawing.Size(28, 23);
            this.btnCompareResultstoClipBoard.TabIndex = 7;
            this.btnCompareResultstoClipBoard.UseVisualStyleBackColor = true;
            this.btnCompareResultstoClipBoard.Click += new System.EventHandler(this.btnCompareResultstoClipBoard_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(69, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(997, 87);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Compare Error";
            this.columnHeader2.Width = 660;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 39);
            this.label3.MaximumSize = new System.Drawing.Size(50, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Compare Result";
            // 
            // txtTestID
            // 
            this.txtTestID.Location = new System.Drawing.Point(358, 3);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.ReadOnly = true;
            this.txtTestID.Size = new System.Drawing.Size(269, 20);
            this.txtTestID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID";
            // 
            // txtTestDescription
            // 
            this.txtTestDescription.Location = new System.Drawing.Point(69, 4);
            this.txtTestDescription.Name = "txtTestDescription";
            this.txtTestDescription.ReadOnly = true;
            this.txtTestDescription.Size = new System.Drawing.Size(259, 20);
            this.txtTestDescription.TabIndex = 1;
            // 
            // Test
            // 
            this.Test.AutoSize = true;
            this.Test.Location = new System.Drawing.Point(14, 11);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(28, 13);
            this.Test.TabIndex = 0;
            this.Test.Text = "Test";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripButton,
            this.toolStripSeparator,
            this.toolStripComboBoxResults,
            this.toolStripDropDownButtonShowCellProp,
            this.toolStripSeparator3,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1078, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileToolStripButton
            // 
            this.fileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testsToolStripMenu,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripButton.Image")));
            this.fileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileToolStripButton.Name = "fileToolStripButton";
            this.fileToolStripButton.Size = new System.Drawing.Size(57, 22);
            this.fileToolStripButton.Text = "&Results";
            // 
            // testsToolStripMenu
            // 
            this.testsToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testspassToolStripMenuItem,
            this.testsfailToolStripMenuItem,
            this.testsuniqueToolStripMenuItem});
            this.testsToolStripMenu.Name = "testsToolStripMenu";
            this.testsToolStripMenu.Size = new System.Drawing.Size(101, 22);
            this.testsToolStripMenu.Text = "Tests";
            // 
            // testspassToolStripMenuItem
            // 
            this.testspassToolStripMenuItem.Name = "testspassToolStripMenuItem";
            this.testspassToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.testspassToolStripMenuItem.Text = "Pass";
            // 
            // testsfailToolStripMenuItem
            // 
            this.testsfailToolStripMenuItem.Name = "testsfailToolStripMenuItem";
            this.testsfailToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.testsfailToolStripMenuItem.Text = "Fail";
            // 
            // testsuniqueToolStripMenuItem
            // 
            this.testsuniqueToolStripMenuItem.Name = "testsuniqueToolStripMenuItem";
            this.testsuniqueToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.testsuniqueToolStripMenuItem.Text = "Unique";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(98, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBoxResults
            // 
            this.toolStripComboBoxResults.Name = "toolStripComboBoxResults";
            this.toolStripComboBoxResults.Size = new System.Drawing.Size(450, 25);
            this.toolStripComboBoxResults.DropDown += new System.EventHandler(this.toolStripComboBoxResults_DropDown);
            this.toolStripComboBoxResults.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripDropDownButtonShowCellProp
            // 
            this.toolStripDropDownButtonShowCellProp.Image = global::SSASRegressionUI.Properties.Resources.toolStripDropDownButtonShowCellProp_Image;
            this.toolStripDropDownButtonShowCellProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonShowCellProp.Name = "toolStripDropDownButtonShowCellProp";
            this.toolStripDropDownButtonShowCellProp.Size = new System.Drawing.Size(136, 22);
            this.toolStripDropDownButtonShowCellProp.Text = "Show Cell Property";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tag = "0";
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmCompareResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCompareResults";
            this.Text = "Compare Results";
            this.Resize += new System.EventHandler(this.frmCompareResults_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTestID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTestDescription;
        private System.Windows.Forms.Label Test;
        private DataGridViewCellSetAdvanced dataGridViewCellSetLeft;
        private DataGridViewCellSetAdvanced dataGridViewCellSetRight;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton fileToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxResults;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonShowCellProp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnCompareResultstoClipBoard;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem testspassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsfailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsuniqueToolStripMenuItem;
    }
}