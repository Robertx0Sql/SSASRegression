using System.Windows.Forms;
using System.Drawing;
namespace MDXStudio
{
    partial class MessagesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.dgvSeverity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMessagesLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMessagesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMessagesMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMessagesLink = new System.Windows.Forms.DataGridViewLinkColumn();
            
            this.SuspendLayout();
            // 
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMessages.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSeverity,
            this.dgvMessagesLine,
            this.dgvMessagesColumn,
            this.dgvMessagesMessage,
            this.dgvMessagesLink});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMessages.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(0, 0);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMessages.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMessages.RowHeadersVisible = false;
            this.dgvMessages.RowHeadersWidth = 20;
            this.dgvMessages.Size = new System.Drawing.Size(556, 134);
            this.dgvMessages.TabIndex = 8;
            this.dgvMessages.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMessages_CellContentClick);
            // 
            // dgvSeverity
            // 
            this.dgvSeverity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvSeverity.HeaderText = "Severity";
            this.dgvSeverity.Name = "dgvSeverity";
            this.dgvSeverity.ReadOnly = true;
            this.dgvSeverity.Width = 70;
            // 
            // dgvMessagesLine
            // 
            this.dgvMessagesLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvMessagesLine.HeaderText = "Line";
            this.dgvMessagesLine.Name = "dgvMessagesLine";
            this.dgvMessagesLine.ReadOnly = true;
            this.dgvMessagesLine.Width = 52;
            // 
            // dgvMessagesColumn
            // 
            this.dgvMessagesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvMessagesColumn.HeaderText = "Column";
            this.dgvMessagesColumn.Name = "dgvMessagesColumn";
            this.dgvMessagesColumn.ReadOnly = true;
            this.dgvMessagesColumn.Width = 67;
            // 
            // dgvMessagesMessage
            // 
            this.dgvMessagesMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMessagesMessage.HeaderText = "Message";
            this.dgvMessagesMessage.Name = "dgvMessagesMessage";
            this.dgvMessagesMessage.ReadOnly = true;
            // 
            // dgvMessagesLink
            // 
            this.dgvMessagesLink.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvMessagesLink.HeaderText = "Link";
            this.dgvMessagesLink.Name = "dgvMessagesLink";
            this.dgvMessagesLink.ReadOnly = true;
            this.dgvMessagesLink.Width = 33;
            // 
            // MessagesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvMessages);
            this.Name = "MessagesControl";
            this.Size = new System.Drawing.Size(556, 134);
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMessages;
        private DataGridViewTextBoxColumn dgvSeverity;
        private DataGridViewTextBoxColumn dgvMessagesLine;
        private DataGridViewTextBoxColumn dgvMessagesColumn;
        private DataGridViewTextBoxColumn dgvMessagesMessage;
        private DataGridViewLinkColumn dgvMessagesLink;
        /*this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.dgvMessages.Location = new System.Drawing.Point(0, 0);
                    this.dgvMessages.Name = "dgvMessages";
                    this.dgvMessages.Size = new System.Drawing.Size(556, 134);
                    this.dgvMessages.TabIndex = 0;
                    */
    }
}
