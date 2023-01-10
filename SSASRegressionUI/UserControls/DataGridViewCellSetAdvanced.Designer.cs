namespace SSASRegressionUI
{
    partial class DataGridViewCellSetAdvanced
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGridCellErrors = new System.Windows.Forms.Label();
            this.btnShowDataElement = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtQueryInfo = new System.Windows.Forms.TextBox();
            this.LblQueryInfo = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.btnShowMdx = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.gridDataTable_old = new System.Windows.Forms.DataGridView();
            this.cellSetDataGridView1 = new VirtualDataGridView.CellSetDataGridView();
            this.gridDataTable = new VirtualDataGridView.SimpleDataGridView();
            this.panel1.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblGridCellErrors);
            this.panel1.Controls.Add(this.btnShowDataElement);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.txtQueryInfo);
            this.panel1.Controls.Add(this.LblQueryInfo);
            this.panel1.Controls.Add(this.txtEndDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtStartDate);
            this.panel1.Controls.Add(this.btnShowMdx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDataBase);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 70);
            this.panel1.TabIndex = 3;
            // 
            // lblGridCellErrors
            // 
            this.lblGridCellErrors.AutoSize = true;
            this.lblGridCellErrors.BackColor = System.Drawing.Color.Yellow;
            this.lblGridCellErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridCellErrors.Location = new System.Drawing.Point(430, 50);
            this.lblGridCellErrors.Name = "lblGridCellErrors";
            this.lblGridCellErrors.Size = new System.Drawing.Size(87, 13);
            this.lblGridCellErrors.TabIndex = 13;
            this.lblGridCellErrors.Text = "#Error in Cells";
            // 
            // btnShowDataElement
            // 
            this.btnShowDataElement.Location = new System.Drawing.Point(230, 0);
            this.btnShowDataElement.Name = "btnShowDataElement";
            this.btnShowDataElement.Size = new System.Drawing.Size(22, 23);
            this.btnShowDataElement.TabIndex = 12;
            this.btnShowDataElement.Text = "D";
            this.btnShowDataElement.UseVisualStyleBackColor = true;
            this.btnShowDataElement.Click += new System.EventHandler(this.btnShowDataElement_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "FileName";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(63, 47);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(361, 20);
            this.txtFileName.TabIndex = 10;
            // 
            // txtQueryInfo
            // 
            this.txtQueryInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryInfo.Location = new System.Drawing.Point(299, 25);
            this.txtQueryInfo.Name = "txtQueryInfo";
            this.txtQueryInfo.ReadOnly = true;
            this.txtQueryInfo.Size = new System.Drawing.Size(231, 20);
            this.txtQueryInfo.TabIndex = 9;
            // 
            // LblQueryInfo
            // 
            this.LblQueryInfo.AutoSize = true;
            this.LblQueryInfo.Location = new System.Drawing.Point(236, 29);
            this.LblQueryInfo.Name = "LblQueryInfo";
            this.LblQueryInfo.Size = new System.Drawing.Size(64, 13);
            this.LblQueryInfo.TabIndex = 8;
            this.LblQueryInfo.Text = "Query Time:";
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(419, 3);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(110, 20);
            this.txtEndDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Start";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtStartDate
            // 
            this.txtStartDate.Location = new System.Drawing.Point(299, 3);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.ReadOnly = true;
            this.txtStartDate.Size = new System.Drawing.Size(114, 20);
            this.txtStartDate.TabIndex = 5;
            // 
            // btnShowMdx
            // 
            this.btnShowMdx.Enabled = false;
            this.btnShowMdx.Location = new System.Drawing.Point(181, 0);
            this.btnShowMdx.Name = "btnShowMdx";
            this.btnShowMdx.Size = new System.Drawing.Size(49, 23);
            this.btnShowMdx.TabIndex = 4;
            this.btnShowMdx.Text = "MDX";
            this.btnShowMdx.UseVisualStyleBackColor = true;
            this.btnShowMdx.Click += new System.EventHandler(this.btnViewMdx_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server";
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(63, 25);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.ReadOnly = true;
            this.txtDataBase.Size = new System.Drawing.Size(167, 20);
            this.txtDataBase.TabIndex = 1;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(63, 2);
            this.txtServer.Name = "txtServer";
            this.txtServer.ReadOnly = true;
            this.txtServer.Size = new System.Drawing.Size(112, 20);
            this.txtServer.TabIndex = 0;
            // 
            // gridDataTable_old
            // 
            this.gridDataTable_old.AllowUserToAddRows = false;
            this.gridDataTable_old.AllowUserToDeleteRows = false;
            this.gridDataTable_old.AllowUserToResizeRows = false;
            this.gridDataTable_old.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridDataTable_old.BackgroundColor = System.Drawing.Color.LightGray;
            this.gridDataTable_old.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDataTable_old.ColumnHeadersVisible = false;
            this.gridDataTable_old.Location = new System.Drawing.Point(294, 125);
            this.gridDataTable_old.Name = "gridDataTable_old";
            this.gridDataTable_old.ReadOnly = true;
            this.gridDataTable_old.RowHeadersVisible = false;
            this.gridDataTable_old.Size = new System.Drawing.Size(119, 77);
            this.gridDataTable_old.TabIndex = 5;
            this.gridDataTable_old.Visible = false;
            // 
            // cellSetDataGridView1
            // 
            this.cellSetDataGridView1.CellProperty = null;
            this.cellSetDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cellSetDataGridView1.Location = new System.Drawing.Point(0, 70);
            this.cellSetDataGridView1.Name = "cellSetDataGridView1";
            this.cellSetDataGridView1.Size = new System.Drawing.Size(533, 247);
            this.cellSetDataGridView1.TabIndex = 4;
            // 
            // gridDataTable
            // 
            this.gridDataTable.CellProperty = null;
            this.gridDataTable.Location = new System.Drawing.Point(102, 125);
            this.gridDataTable.Name = "gridDataTable";
            this.gridDataTable.Size = new System.Drawing.Size(150, 150);
            this.gridDataTable.TabIndex = 6;
            this.gridDataTable.Visible = false;
            // 
            // DataGridViewCellSetAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridDataTable);
            this.Controls.Add(this.gridDataTable_old);
            this.Controls.Add(this.cellSetDataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "DataGridViewCellSetAdvanced";
            this.Size = new System.Drawing.Size(533, 317);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblQueryInfo;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.Button btnShowMdx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.TextBox txtServer;
        private VirtualDataGridView.CellSetDataGridView cellSetDataGridView1;
        private System.Windows.Forms.TextBox txtQueryInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnShowDataElement;
        private System.Windows.Forms.DataGridView gridDataTable_old;
        private System.Windows.Forms.Label lblGridCellErrors;
        private VirtualDataGridView.SimpleDataGridView gridDataTable;
    }
}
