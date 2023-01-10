namespace VirtualDataGridView
{
    partial class SimpleDataGridView
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
            this.gridDataTable = new System.Windows.Forms.DataGridView();
            
            this.SuspendLayout();
            // 
            // gridDataTable
            // 
            this.gridDataTable.AllowUserToAddRows = false;
            this.gridDataTable.AllowUserToDeleteRows = false;
            this.gridDataTable.AllowUserToResizeRows = false;
            this.gridDataTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridDataTable.BackgroundColor = System.Drawing.Color.LightGray;
            this.gridDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDataTable.ColumnHeadersVisible = false;
            this.gridDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDataTable.Location = new System.Drawing.Point(0, 0);
            this.gridDataTable.Name = "gridDataTable";
            this.gridDataTable.ReadOnly = true;
            this.gridDataTable.RowHeadersVisible = false;
            this.gridDataTable.Size = new System.Drawing.Size(349, 243);
            this.gridDataTable.TabIndex = 6;
            this.gridDataTable.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RaiseEventCellEnter);
            // 
            // SimpleDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridDataTable);
            this.Name = "SimpleDataGridView";
            this.Size = new System.Drawing.Size(349, 243);
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridDataTable;
    }
}
