namespace SSASRegressionUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label3 = new System.Windows.Forms.Label();
            this.txtTestFilePath = new System.Windows.Forms.TextBox();
            this.btnTestFilePath = new System.Windows.Forms.Button();
            this.btnEditTestFile = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCreateNewTests = new System.Windows.Forms.Button();
            this.groupBoxTests = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnConnectServer = new System.Windows.Forms.Button();
            this.cboCatalogs = new System.Windows.Forms.ComboBox();
            this.txtEffectiveUserName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rbConnectionString = new System.Windows.Forms.RadioButton();
            this.rbConnProperties = new System.Windows.Forms.RadioButton();
            this.btnResultsViewLatest = new System.Windows.Forms.Button();
            this.numParallelQueries = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRunTests = new System.Windows.Forms.Button();
            this.txtResultFilePath = new System.Windows.Forms.TextBox();
            this.btnResultFilePath = new System.Windows.Forms.Button();
            this.groupBoxCompare = new System.Windows.Forms.GroupBox();
            this.btnResultsView2 = new System.Windows.Forms.Button();
            this.btnResultsView1 = new System.Windows.Forms.Button();
            this.btnCompareData = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCompareResultFilePath2 = new System.Windows.Forms.Button();
            this.txtCompareResultFile2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCompareResultFilePath1 = new System.Windows.Forms.Button();
            this.txtCompareResultFile1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.runTesttoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.comparetoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.groupBoxTests.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParallelQueries)).BeginInit();
            this.groupBoxCompare.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Test File ";
            // 
            // txtTestFilePath
            // 
            this.txtTestFilePath.AllowDrop = true;
            this.txtTestFilePath.Location = new System.Drawing.Point(83, 19);
            this.txtTestFilePath.Name = "txtTestFilePath";
            this.txtTestFilePath.Size = new System.Drawing.Size(281, 20);
            this.txtTestFilePath.TabIndex = 1;
            this.txtTestFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.txtTestFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // btnTestFilePath
            // 
            this.btnTestFilePath.Location = new System.Drawing.Point(368, 16);
            this.btnTestFilePath.Name = "btnTestFilePath";
            this.btnTestFilePath.Size = new System.Drawing.Size(22, 23);
            this.btnTestFilePath.TabIndex = 2;
            this.btnTestFilePath.Text = "...";
            this.btnTestFilePath.UseVisualStyleBackColor = true;
            this.btnTestFilePath.Click += new System.EventHandler(this.btnTestFilePath_Click);
            // 
            // btnEditTestFile
            // 
            this.btnEditTestFile.Location = new System.Drawing.Point(431, 16);
            this.btnEditTestFile.Name = "btnEditTestFile";
            this.btnEditTestFile.Size = new System.Drawing.Size(65, 23);
            this.btnEditTestFile.TabIndex = 3;
            this.btnEditTestFile.Text = "&Edit Tests";
            this.btnEditTestFile.UseVisualStyleBackColor = true;
            this.btnEditTestFile.Click += new System.EventHandler(this.btnEditTestFile_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 476);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // btnCreateNewTests
            // 
            this.btnCreateNewTests.Location = new System.Drawing.Point(502, 16);
            this.btnCreateNewTests.Name = "btnCreateNewTests";
            this.btnCreateNewTests.Size = new System.Drawing.Size(72, 23);
            this.btnCreateNewTests.TabIndex = 4;
            this.btnCreateNewTests.Text = "&New Tests";
            this.btnCreateNewTests.UseVisualStyleBackColor = true;
            this.btnCreateNewTests.Click += new System.EventHandler(this.btnCreateNewTests_Click);
            // 
            // groupBoxTests
            // 
            this.groupBoxTests.Controls.Add(this.tabControl1);
            this.groupBoxTests.Controls.Add(this.rbConnectionString);
            this.groupBoxTests.Controls.Add(this.rbConnProperties);
            this.groupBoxTests.Controls.Add(this.btnResultsViewLatest);
            this.groupBoxTests.Controls.Add(this.numParallelQueries);
            this.groupBoxTests.Controls.Add(this.label8);
            this.groupBoxTests.Controls.Add(this.label4);
            this.groupBoxTests.Controls.Add(this.btnRunTests);
            this.groupBoxTests.Controls.Add(this.txtResultFilePath);
            this.groupBoxTests.Controls.Add(this.btnResultFilePath);
            this.groupBoxTests.Controls.Add(this.label3);
            this.groupBoxTests.Controls.Add(this.btnCreateNewTests);
            this.groupBoxTests.Controls.Add(this.btnEditTestFile);
            this.groupBoxTests.Controls.Add(this.txtTestFilePath);
            this.groupBoxTests.Controls.Add(this.btnTestFilePath);
            this.groupBoxTests.Location = new System.Drawing.Point(12, 27);
            this.groupBoxTests.Name = "groupBoxTests";
            this.groupBoxTests.Size = new System.Drawing.Size(589, 203);
            this.groupBoxTests.TabIndex = 0;
            this.groupBoxTests.TabStop = false;
            this.groupBoxTests.Text = "Tests";
            this.groupBoxTests.Enter += new System.EventHandler(this.groupBoxTests_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(420, 109);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cboServer);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btnConnectServer);
            this.tabPage1.Controls.Add(this.cboCatalogs);
            this.tabPage1.Controls.Add(this.txtEffectiveUserName);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(412, 80);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server / Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Database";
            // 
            // cboServer
            // 
            this.cboServer.FormattingEnabled = true;
            this.cboServer.Location = new System.Drawing.Point(71, 2);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(254, 21);
            this.cboServer.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "User Name";
            // 
            // btnConnectServer
            // 
            this.btnConnectServer.Location = new System.Drawing.Point(331, 0);
            this.btnConnectServer.Name = "btnConnectServer";
            this.btnConnectServer.Size = new System.Drawing.Size(75, 23);
            this.btnConnectServer.TabIndex = 6;
            this.btnConnectServer.Text = "Connect";
            this.btnConnectServer.UseVisualStyleBackColor = true;
            this.btnConnectServer.Click += new System.EventHandler(this.btnConnectServer_Click);
            // 
            // cboCatalogs
            // 
            this.cboCatalogs.FormattingEnabled = true;
            this.cboCatalogs.Location = new System.Drawing.Point(71, 29);
            this.cboCatalogs.Name = "cboCatalogs";
            this.cboCatalogs.Size = new System.Drawing.Size(335, 21);
            this.cboCatalogs.TabIndex = 7;
            // 
            // txtEffectiveUserName
            // 
            this.txtEffectiveUserName.Location = new System.Drawing.Point(71, 56);
            this.txtEffectiveUserName.Name = "txtEffectiveUserName";
            this.txtEffectiveUserName.Size = new System.Drawing.Size(208, 20);
            this.txtEffectiveUserName.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtConnectionString);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(412, 80);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Connection String";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(71, 0);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(332, 79);
            this.txtConnectionString.TabIndex = 9;
            this.txtConnectionString.Text = "Provider=MSOLAP.4;Server";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Connection";
            // 
            // rbConnectionString
            // 
            this.rbConnectionString.AutoSize = true;
            this.rbConnectionString.Location = new System.Drawing.Point(453, 92);
            this.rbConnectionString.Name = "rbConnectionString";
            this.rbConnectionString.Size = new System.Drawing.Size(109, 17);
            this.rbConnectionString.TabIndex = 35;
            this.rbConnectionString.Text = "Connection String";
            this.rbConnectionString.UseVisualStyleBackColor = true;
            this.rbConnectionString.Visible = false;
            this.rbConnectionString.CheckedChanged += new System.EventHandler(this.rbConnectionString_CheckedChanged);
            // 
            // rbConnProperties
            // 
            this.rbConnProperties.AutoSize = true;
            this.rbConnProperties.Checked = true;
            this.rbConnProperties.Location = new System.Drawing.Point(454, 75);
            this.rbConnProperties.Name = "rbConnProperties";
            this.rbConnProperties.Size = new System.Drawing.Size(97, 17);
            this.rbConnProperties.TabIndex = 34;
            this.rbConnProperties.TabStop = true;
            this.rbConnProperties.Text = "Server/Catalog";
            this.rbConnProperties.UseVisualStyleBackColor = true;
            this.rbConnProperties.Visible = false;
            this.rbConnProperties.CheckedChanged += new System.EventHandler(this.rbConnProperties_CheckedChanged);
            // 
            // btnResultsViewLatest
            // 
            this.btnResultsViewLatest.Location = new System.Drawing.Point(396, 164);
            this.btnResultsViewLatest.Name = "btnResultsViewLatest";
            this.btnResultsViewLatest.Size = new System.Drawing.Size(22, 23);
            this.btnResultsViewLatest.TabIndex = 12;
            this.btnResultsViewLatest.Text = "V";
            this.btnResultsViewLatest.UseVisualStyleBackColor = true;
            this.btnResultsViewLatest.Click += new System.EventHandler(this.btnResultsViewLatest_Click);
            // 
            // numParallelQueries
            // 
            this.numParallelQueries.Location = new System.Drawing.Point(532, 134);
            this.numParallelQueries.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numParallelQueries.Name = "numParallelQueries";
            this.numParallelQueries.Size = new System.Drawing.Size(42, 20);
            this.numParallelQueries.TabIndex = 8;
            this.numParallelQueries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(442, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Parallel Queries";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Results File";
            // 
            // btnRunTests
            // 
            this.btnRunTests.Location = new System.Drawing.Point(467, 164);
            this.btnRunTests.Name = "btnRunTests";
            this.btnRunTests.Size = new System.Drawing.Size(107, 23);
            this.btnRunTests.TabIndex = 13;
            this.btnRunTests.Text = "&Run Tests";
            this.btnRunTests.UseVisualStyleBackColor = true;
            this.btnRunTests.Click += new System.EventHandler(this.btnRunTests_Click);
            // 
            // txtResultFilePath
            // 
            this.txtResultFilePath.AllowDrop = true;
            this.txtResultFilePath.Location = new System.Drawing.Point(83, 164);
            this.txtResultFilePath.Name = "txtResultFilePath";
            this.txtResultFilePath.Size = new System.Drawing.Size(281, 20);
            this.txtResultFilePath.TabIndex = 10;
            this.txtResultFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.txtResultFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // btnResultFilePath
            // 
            this.btnResultFilePath.Location = new System.Drawing.Point(368, 164);
            this.btnResultFilePath.Name = "btnResultFilePath";
            this.btnResultFilePath.Size = new System.Drawing.Size(22, 23);
            this.btnResultFilePath.TabIndex = 11;
            this.btnResultFilePath.Text = "...";
            this.btnResultFilePath.UseVisualStyleBackColor = true;
            this.btnResultFilePath.Click += new System.EventHandler(this.btnResultFilePath_Click);
            // 
            // groupBoxCompare
            // 
            this.groupBoxCompare.Controls.Add(this.btnResultsView2);
            this.groupBoxCompare.Controls.Add(this.btnResultsView1);
            this.groupBoxCompare.Controls.Add(this.btnCompareData);
            this.groupBoxCompare.Controls.Add(this.label7);
            this.groupBoxCompare.Controls.Add(this.btnCompareResultFilePath2);
            this.groupBoxCompare.Controls.Add(this.txtCompareResultFile2);
            this.groupBoxCompare.Controls.Add(this.label6);
            this.groupBoxCompare.Controls.Add(this.btnCompareResultFilePath1);
            this.groupBoxCompare.Controls.Add(this.txtCompareResultFile1);
            this.groupBoxCompare.Controls.Add(this.label5);
            this.groupBoxCompare.Location = new System.Drawing.Point(12, 236);
            this.groupBoxCompare.Name = "groupBoxCompare";
            this.groupBoxCompare.Size = new System.Drawing.Size(589, 108);
            this.groupBoxCompare.TabIndex = 2;
            this.groupBoxCompare.TabStop = false;
            this.groupBoxCompare.Text = "Compare Results";
            // 
            // btnResultsView2
            // 
            this.btnResultsView2.Location = new System.Drawing.Point(396, 56);
            this.btnResultsView2.Name = "btnResultsView2";
            this.btnResultsView2.Size = new System.Drawing.Size(22, 23);
            this.btnResultsView2.TabIndex = 5;
            this.btnResultsView2.Text = "V";
            this.btnResultsView2.UseVisualStyleBackColor = true;
            this.btnResultsView2.Click += new System.EventHandler(this.btnResultsView2_Click);
            // 
            // btnResultsView1
            // 
            this.btnResultsView1.Location = new System.Drawing.Point(396, 30);
            this.btnResultsView1.Name = "btnResultsView1";
            this.btnResultsView1.Size = new System.Drawing.Size(22, 23);
            this.btnResultsView1.TabIndex = 2;
            this.btnResultsView1.Text = "V";
            this.btnResultsView1.UseVisualStyleBackColor = true;
            this.btnResultsView1.Click += new System.EventHandler(this.btnResultsView1_Click);
            // 
            // btnCompareData
            // 
            this.btnCompareData.Location = new System.Drawing.Point(467, 56);
            this.btnCompareData.Name = "btnCompareData";
            this.btnCompareData.Size = new System.Drawing.Size(107, 23);
            this.btnCompareData.TabIndex = 6;
            this.btnCompareData.Text = "&Compare";
            this.btnCompareData.UseVisualStyleBackColor = true;
            this.btnCompareData.Click += new System.EventHandler(this.btnCompareData_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Comparison";
            this.label7.Visible = false;
            // 
            // btnCompareResultFilePath2
            // 
            this.btnCompareResultFilePath2.Location = new System.Drawing.Point(368, 56);
            this.btnCompareResultFilePath2.Name = "btnCompareResultFilePath2";
            this.btnCompareResultFilePath2.Size = new System.Drawing.Size(22, 23);
            this.btnCompareResultFilePath2.TabIndex = 4;
            this.btnCompareResultFilePath2.Text = "...";
            this.btnCompareResultFilePath2.UseVisualStyleBackColor = true;
            this.btnCompareResultFilePath2.Click += new System.EventHandler(this.btnCompareResultFilePath2_Click);
            // 
            // txtCompareResultFile2
            // 
            this.txtCompareResultFile2.AllowDrop = true;
            this.txtCompareResultFile2.Location = new System.Drawing.Point(83, 56);
            this.txtCompareResultFile2.Name = "txtCompareResultFile2";
            this.txtCompareResultFile2.Size = new System.Drawing.Size(281, 20);
            this.txtCompareResultFile2.TabIndex = 3;
            this.txtCompareResultFile2.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.txtCompareResultFile2.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Results File 2";
            // 
            // btnCompareResultFilePath1
            // 
            this.btnCompareResultFilePath1.Location = new System.Drawing.Point(368, 30);
            this.btnCompareResultFilePath1.Name = "btnCompareResultFilePath1";
            this.btnCompareResultFilePath1.Size = new System.Drawing.Size(22, 23);
            this.btnCompareResultFilePath1.TabIndex = 1;
            this.btnCompareResultFilePath1.Text = "...";
            this.btnCompareResultFilePath1.UseVisualStyleBackColor = true;
            this.btnCompareResultFilePath1.Click += new System.EventHandler(this.btnCompareResultFilePath1_Click);
            // 
            // txtCompareResultFile1
            // 
            this.txtCompareResultFile1.AllowDrop = true;
            this.txtCompareResultFile1.Location = new System.Drawing.Point(83, 30);
            this.txtCompareResultFile1.Name = "txtCompareResultFile1";
            this.txtCompareResultFile1.Size = new System.Drawing.Size(281, 20);
            this.txtCompareResultFile1.TabIndex = 0;
            this.txtCompareResultFile1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.txtCompareResultFile1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Results File 1";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(12, 350);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(589, 123);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DateTime";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 495;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestToolStripMenuItem,
            this.newTestToolStripMenuItem,
            this.toolStripSeparator,
            this.runTesttoolStripMenuItem1,
            this.comparetoolStripMenuItem2,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editTestToolStripMenuItem
            // 
            this.editTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editTestToolStripMenuItem.Image")));
            this.editTestToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTestToolStripMenuItem.Name = "editTestToolStripMenuItem";
            this.editTestToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editTestToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.editTestToolStripMenuItem.Text = "&Edit Tests";
            this.editTestToolStripMenuItem.Click += new System.EventHandler(this.editTestToolStripMenuItem_Click);
            // 
            // newTestToolStripMenuItem
            // 
            this.newTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newTestToolStripMenuItem.Image")));
            this.newTestToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTestToolStripMenuItem.Name = "newTestToolStripMenuItem";
            this.newTestToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newTestToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newTestToolStripMenuItem.Text = "&New Tests";
            this.newTestToolStripMenuItem.Click += new System.EventHandler(this.newTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(183, 6);
            // 
            // runTesttoolStripMenuItem1
            // 
            this.runTesttoolStripMenuItem1.Name = "runTesttoolStripMenuItem1";
            this.runTesttoolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+R";
            this.runTesttoolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.runTesttoolStripMenuItem1.Text = "&Run Tests";
            this.runTesttoolStripMenuItem1.Click += new System.EventHandler(this.runTesttoolStripMenuItem1_Click);
            // 
            // comparetoolStripMenuItem2
            // 
            this.comparetoolStripMenuItem2.Name = "comparetoolStripMenuItem2";
            this.comparetoolStripMenuItem2.ShortcutKeyDisplayString = "Crtl+M";
            this.comparetoolStripMenuItem2.Size = new System.Drawing.Size(186, 22);
            this.comparetoolStripMenuItem2.Text = "Co&mpare Tests";
            this.comparetoolStripMenuItem2.Click += new System.EventHandler(this.comparetoolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 498);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBoxCompare);
            this.Controls.Add(this.groupBoxTests);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Regression UI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxTests.ResumeLayout(false);
            this.groupBoxTests.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParallelQueries)).EndInit();
            this.groupBoxCompare.ResumeLayout(false);
            this.groupBoxCompare.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTestFilePath;
        private System.Windows.Forms.Button btnTestFilePath;
        private System.Windows.Forms.Button btnEditTestFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnCreateNewTests;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBoxTests;
        private System.Windows.Forms.GroupBox groupBoxCompare;
        private System.Windows.Forms.Button btnCompareResultFilePath2;
        private System.Windows.Forms.TextBox txtCompareResultFile2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCompareResultFilePath1;
        private System.Windows.Forms.TextBox txtCompareResultFile1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCompareData;
        private System.Windows.Forms.Button btnResultsView2;
        private System.Windows.Forms.Button btnResultsView1;
        private System.Windows.Forms.NumericUpDown numParallelQueries;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnConnectServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboCatalogs;
        private System.Windows.Forms.Button btnRunTests;
        private System.Windows.Forms.TextBox txtResultFilePath;
        private System.Windows.Forms.Button btnResultFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.Button btnResultsViewLatest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEffectiveUserName;
        private System.Windows.Forms.ToolStripMenuItem runTesttoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem comparetoolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton rbConnectionString;
        private System.Windows.Forms.RadioButton rbConnProperties;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

